using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Sales.Domain.Entities
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int PizzaId { get; set; }
        public int PizzaTypeId { get; set; }
        public string Size { get; set; }
        public decimal Price { get; set; }
        public int OrderId { get; set; }

        public Pizza Pizza { get; set; }
        public PizzaType PizzaType { get; set; }
        public Order Order { get; set; }
    }
}
