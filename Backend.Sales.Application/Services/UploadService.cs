using Backend.Domain.Interface;
using Backend.Sales.Application.Interface;
using Backend.Sales.Application.Models;
using Backend.Sales.Domain.Entities;
using CsvHelper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Data;
using System.Globalization;
using Entities = Backend.Sales.Domain.Entities;
using Enums = Backend.Sales.Domain.Enumerations;

namespace Backend.Sales.Application.Services
{
    public class UploadService: IUploadService
    {
        private readonly ISaleDbContext _context;
        private readonly ILogger _logger;
        public UploadService(ISaleDbContext context, ILogger<UploadService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task ProcessCsvFileAsync(Stream csvStream, Enums.EntityType entityType)
        {
            using (var reader = new StreamReader(csvStream))
            using (var csv = new CsvReader(reader, new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                // Register the appropriate map based on entity type
                try
                {
                    switch (entityType)
                    {
                        case Enums.EntityType.Pizza:
                            csv.Context.RegisterClassMap<PizzaCsvModelMap>();
                            break;
                        case Enums.EntityType.PizzaType:
                            csv.Context.RegisterClassMap<PizzaTypeCsvModelMap>();
                            break;
                        case Enums.EntityType.OrderDetail:
                            csv.Context.RegisterClassMap<OrderDetailCsvModelMap>();
                            break;
                        case Enums.EntityType.Order:
                            csv.Context.RegisterClassMap<OrderCsvModelMap>();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while processing the request");
                }

                var rowIndex = 1; // Assuming the first row is headers, start counting from 1

                while (await csv.ReadAsync())
                {
                    try
                    {
                        await InsertRecordAsync(csv, entityType);
                    }
                    catch (Exception ex)
                    {
                        var message = "";
                        if (ex.InnerException != null)
                        {
                            message += ex.InnerException.Message;
                        }

                        message += ex.Message;

                        _logger.LogError(ex, message);
                    }

                    rowIndex++;
                }
            }
        }

        private async Task InsertRecordAsync(CsvReader csv, Enums.EntityType entityType)
        {
            switch (entityType)
            {
                case Enums.EntityType.Pizza:
                    var pizzaRecord = csv.GetRecord<Models.PizzaCsvModel>();
                    await InsertPizzasAsync(new List<Models.PizzaCsvModel> { pizzaRecord });
                    break;

                case Enums.EntityType.PizzaType:
                    var pizzaTypeRecord = csv.GetRecord<Models.PizzaTypeCsvModel>();
                    await InsertPizzaTypesAsync(new List<Models.PizzaTypeCsvModel> { pizzaTypeRecord });
                    break;

                case Enums.EntityType.Order:
                    var orderRecord = csv.GetRecord<Models.OrderCsvModel>();
                    await InsertOrdersAsync(new List<Models.OrderCsvModel> { orderRecord });
                    break;

                case Enums.EntityType.OrderDetail:
                    var orderDetailRecord = csv.GetRecord<Models.OrderDetailCsvModel>();
                    await InsertOrderDetailsAsync(new List<Models.OrderDetailCsvModel> { orderDetailRecord });
                    break;

                default:
                    throw new ArgumentException("Invalid file type.");
            }
        }

        private async Task InsertPizzasAsync(IEnumerable<Models.PizzaCsvModel> pizzas)
        {
            // Create a DataTable that matches the structure of dbo.PizzaTableType
            var pizzaTable = new DataTable();
            pizzaTable.Columns.Add("PizzaId", typeof(string));
            pizzaTable.Columns.Add("PizzaTypeId", typeof(string));
            pizzaTable.Columns.Add("Size", typeof(string));
            pizzaTable.Columns.Add("Price", typeof(decimal));

            // Populate the DataTable with data from the pizzas collection
            foreach (var pizza in pizzas)
            {
                pizzaTable.Rows.Add(pizza.PizzaId, pizza.PizzaTypeId, pizza.Size, pizza.Price);
            }

            // Create a SqlParameter for the TVP
            var pizzaTableParam = new SqlParameter("@PizzaTable", SqlDbType.Structured)
            {
                TypeName = "dbo.PizzaTableType",
                Value = pizzaTable
            };

            // Execute the stored procedure
            await _context.ExecuteSqlRawAsync("EXEC InsertPizzas @PizzaTable", pizzaTableParam);
        }

        private async Task InsertPizzaTypesAsync(IEnumerable<Models.PizzaTypeCsvModel> pizzaTypes)
        {
            // Create a DataTable that matches the structure of dbo.PizzaTypeTableType
            var pizzaTypeTable = new DataTable();
            pizzaTypeTable.Columns.Add("PizzaTypeId", typeof(string));
            pizzaTypeTable.Columns.Add("Name", typeof(string));
            pizzaTypeTable.Columns.Add("Category", typeof(string));
            pizzaTypeTable.Columns.Add("Ingredients", typeof(string));

            // Populate the DataTable with data from the pizzaTypes collection
            foreach (var pizzaType in pizzaTypes)
            {
                pizzaTypeTable.Rows.Add(pizzaType.PizzaTypeId, pizzaType.Name, pizzaType.Category, pizzaType.Ingredients);
            }

            // Create a SqlParameter for the TVP
            var pizzaTypeTableParam = new SqlParameter("@PizzaTypeTable", SqlDbType.Structured)
            {
                TypeName = "dbo.PizzaTypeTableType",
                Value = pizzaTypeTable
            };

            // Execute the stored procedure
            await _context.ExecuteSqlRawAsync("EXEC InsertPizzaTypes @PizzaTypeTable", pizzaTypeTableParam);
        }

        private async Task InsertOrdersAsync(IEnumerable<Models.OrderCsvModel> orders)
        {
            // Create a DataTable that matches the structure of dbo.OrderTableType
            var orderTable = new DataTable();
            orderTable.Columns.Add("OrderId", typeof(int));
            orderTable.Columns.Add("Date", typeof(DateTime));
            orderTable.Columns.Add("Time", typeof(TimeSpan));

            // Populate the DataTable with data from the orders collection
            foreach (var order in orders)
            {
                DateTime dateTime = order.Date.ToDateTime(TimeOnly.MinValue);
                TimeSpan timeSpan = order.Time.ToTimeSpan();
                orderTable.Rows.Add(order.OrderId, dateTime, timeSpan);
            }

            // Create a SqlParameter for the TVP
            var orderTableParam = new SqlParameter("@OrderTable", SqlDbType.Structured)
            {
                TypeName = "dbo.OrderTableType",
                Value = orderTable
            };

            // Execute the stored procedure
            await _context.ExecuteSqlRawAsync("EXEC InsertOrders @OrderTable", orderTableParam);
        }

        private async Task InsertOrderDetailsAsync(IEnumerable<Models.OrderDetailCsvModel> orderDetails)
        {
            // Create a DataTable that matches the structure of dbo.OrderDetailTableType
            var orderDetailTable = new DataTable();
            orderDetailTable.Columns.Add("OrderDetailId", typeof(int));
            orderDetailTable.Columns.Add("PizzaId", typeof(string));
            orderDetailTable.Columns.Add("Quantity", typeof(int));
            orderDetailTable.Columns.Add("OrderId", typeof(int));

            // Populate the DataTable with data from the orderDetails collection
            foreach (var orderDetail in orderDetails)
            {
                orderDetailTable.Rows.Add(orderDetail.PizzaId, orderDetail.Quantity, orderDetail.OrderId);
            }

            // Create a SqlParameter for the TVP
            var orderDetailTableParam = new SqlParameter("@OrderDetailTable", SqlDbType.Structured)
            {
                TypeName = "dbo.OrderDetailTableType",
                Value = orderDetailTable
            };

            // Execute the stored procedure
            await _context.ExecuteSqlRawAsync("EXEC InsertOrderDetails @OrderDetailTable", orderDetailTableParam);
        }
    }
}
