using Microsoft.EntityFrameworkCore;
using OrderMngmt.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace OrderMngmt.Data;
public class OrderMngmtDbContext : IdentityDbContext<User> {
    public OrderMngmtDbContext(DbContextOptions<OrderMngmtDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Product 1", Price = 10.00m },
            new Product { Id = 2, Name = "Product 2", Price = 20.00m },
            new Product { Id = 3, Name = "Product 3", Price = 30.00m },
            new Product { Id = 4, Name = "Product 4", Price = 40.00m },
            new Product { Id = 5, Name = "Product 5", Price = 50.00m }
        );

        modelBuilder.Entity<Order>().HasData(
            new Order { Id = 1, UserId = 1, ProductId = 1, Quantity = 1, Total = 10.00m },
            new Order { Id = 2, UserId = 2, ProductId = 2, Quantity = 2, Total = 40.00m },
            new Order { Id = 3, UserId = 3, ProductId = 3, Quantity = 3, Total = 90.00m },
            new Order { Id = 4, UserId = 4, ProductId = 4, Quantity = 4, Total = 160.00m },
            new Order { Id = 5, UserId = 5, ProductId = 5, Quantity = 5, Total = 250.00m }
        );

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<User> Users { get; set; }

}

