using Backend.Sales.Application.Models;
using CsvHelper.Configuration;

public class OrderDetailCsvModelMap : ClassMap<OrderDetailCsvModel>
{
    public OrderDetailCsvModelMap()
    {
        // Define the mapping between CSV headers and model properties
        Map(m => m.OrderDetailId).Name("order_details_id");
        Map(m => m.OrderId).Name("order_id");
        Map(m => m.PizzaId).Name("pizza_id");
        Map(m => m.Quantity).Name("quantity");
    }
}