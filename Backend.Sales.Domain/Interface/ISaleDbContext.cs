
using Microsoft.EntityFrameworkCore;
using Backend.Sales.Domain.Entities;

namespace Backend.Domain.Interface
{
    public interface ISaleDbContext
    {

        // Add any additional methods if needed
        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<Pizza> Pizzas { get; set; }

        public DbSet<PizzaType> PizzaTypes { get; set; }
        public Task<int> SaveChangesAsync();
    }
}
