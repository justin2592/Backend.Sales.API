using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Sales.Application.Models
{
    public class PizzaType
    {
        public int PizzaTypeId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Ingredients { get; set; }

        public ICollection<Pizza> Pizzas { get; set; }
    }
}
