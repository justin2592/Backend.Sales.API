using Backend.Sales.Application.Converters;
using Backend.Sales.Application.Models;
using CsvHelper.Configuration;

public class OrderCsvModelMap : ClassMap<OrderCsvModel>
{
    public OrderCsvModelMap()
    {
        // Define the mapping between CSV headers and model properties
        Map(m => m.OrderId).Name("order_id");
        Map(m => m.Date).Name("date").TypeConverterOption.Format("yyyy-MM-dd"); // Adjust format as needed
        Map(m => m.Time)
            .Name("time")
            .TypeConverter<TimeOnlyConverter>()
            .TypeConverterOption.Format("hh\\:mm\\:ss");
    }
}