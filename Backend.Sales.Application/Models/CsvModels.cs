using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Sales.Application.Models
{
    public class PizzaCsvModel
    {
        public string PizzaId { get; set; }
        public string PizzaTypeId { get; set; }
        public string Size { get; set; }
        public decimal Price { get; set; }
    }

    public class PizzaTypeCsvModel
    {
        public string PizzaTypeId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Ingredients { get; set; }
    }

    public class OrderDetailCsvModel
    {
        public int OrderDetailId { get; set; }
        public string PizzaId { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }
    }

    public class OrderCsvModel
    {
        public int OrderId { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
    }
}
