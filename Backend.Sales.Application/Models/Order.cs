using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Sales.Application.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateOnly Date { get; set; }
        public TimeSpan Time { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
