using Backend.Sales.Application.Models;
using CsvHelper.Configuration;

public class PizzaTypeCsvModelMap : ClassMap<PizzaTypeCsvModel>
{
    public PizzaTypeCsvModelMap()
    {
        // Define the mapping between CSV headers and model properties
        Map(m => m.PizzaTypeId).Name("pizza_type_id");
        Map(m => m.Name).Name("name");
        Map(m => m.Category).Name("category");
        Map(m => m.Ingredients).Name("ingredients");
    }
}