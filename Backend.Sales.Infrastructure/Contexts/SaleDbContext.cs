using Microsoft.EntityFrameworkCore;
using Backend.Sales.Domain.Entities;
using Microsoft.Extensions.Configuration;
public class SaleDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public SaleDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }


    public DbSet<Pizza> Pizzas { get; set; }
    public DbSet<PizzaType> PizzaTypes { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure primary keys
        modelBuilder.Entity<Pizza>()
            .HasKey(p => p.PizzaId);

        modelBuilder.Entity<PizzaType>()
            .HasKey(pt => pt.PizzaTypeId);

        modelBuilder.Entity<OrderDetail>()
            .HasKey(od => od.OrderDetailId);

        modelBuilder.Entity<Order>()
            .HasKey(o => o.OrderId);

        // Configure relationships
        modelBuilder.Entity<Pizza>()
            .HasOne(p => p.PizzaType)
            .WithMany(pt => pt.Pizzas)
            .HasForeignKey(p => p.PizzaTypeId);

        modelBuilder.Entity<OrderDetail>()
            .HasOne(od => od.Pizza)
            .WithMany(p => p.OrderDetails)
            .HasForeignKey(od => od.PizzaId);

        modelBuilder.Entity<OrderDetail>()
            .HasOne(od => od.PizzaType)
            .WithMany()
            .HasForeignKey(od => od.PizzaTypeId);

        modelBuilder.Entity<OrderDetail>()
            .HasOne(od => od.Order)
            .WithMany(o => o.OrderDetails)
            .HasForeignKey(od => od.OrderId);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = _configuration.GetConnectionString("ConnectionStrings:SmsDBConnection");
        optionsBuilder.UseSqlServer(connectionString);
    }
}