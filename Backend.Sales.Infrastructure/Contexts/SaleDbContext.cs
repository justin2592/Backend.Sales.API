using System;
using System.Collections.Generic;
using Backend.Sales.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Backend.Domain.Interface;

namespace Backend.Sales.Infrastructure.Contexts;

public partial class SaleDbContext : DbContext, ISaleDbContext
{
    private readonly IConfiguration _configuration;
    public SaleDbContext(DbContextOptions<SaleDbContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Pizza> Pizzas { get; set; }

    public virtual DbSet<PizzaType> PizzaTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Order__C3905BCF7F19DBFF");

            entity.ToTable("Order");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailId).HasName("PK__OrderDet__D3B9D36CF8676400");

            entity.ToTable("OrderDetail");

            entity.Property(e => e.PizzaId).HasMaxLength(50);

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderDeta__Order__22751F6C");

            entity.HasOne(d => d.Pizza).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.PizzaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderDeta__Pizza__2180FB33");
        });

        modelBuilder.Entity<Pizza>(entity =>
        {
            entity.HasKey(e => e.PizzaId).HasName("PK__Pizza__0B6012DD8757D2FA");

            entity.ToTable("Pizza");

            entity.Property(e => e.PizzaId).HasMaxLength(50);
            entity.Property(e => e.PizzaTypeId).HasMaxLength(50);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Size).HasMaxLength(50);

            entity.HasOne(d => d.PizzaType).WithMany(p => p.Pizzas)
                .HasForeignKey(d => d.PizzaTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pizza__PizzaType__778AC167");
        });

        modelBuilder.Entity<PizzaType>(entity =>
        {
            entity.HasKey(e => e.PizzaTypeId).HasName("PK__PizzaTyp__126096801F5509F2");

            entity.ToTable("PizzaType");

            entity.Property(e => e.PizzaTypeId).HasMaxLength(50);
            entity.Property(e => e.Category).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public async Task<int> ExecuteSqlRawAsync(string sql, params object[] parameters)
    {
        return await Database.ExecuteSqlRawAsync(sql, parameters);
    }
}
