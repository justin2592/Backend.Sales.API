using Backend.Sales.Application.Models;
using CsvHelper.Configuration;

public class PizzaCsvModelMap : ClassMap<PizzaCsvModel>
{
    public PizzaCsvModelMap()
    {
        // Define the mapping between CSV headers and model properties
        Map(m => m.PizzaId).Name("pizza_id");
        Map(m => m.PizzaTypeId).Name("pizza_type_id");
        Map(m => m.Size).Name("size");
        Map(m => m.Price).Name("price");
    }
}