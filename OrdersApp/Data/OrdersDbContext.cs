using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OrdersApp.Entities;
using System.Configuration;

namespace OrdersApp.Data;

public class OrdersDbContext : DbContext
{
    private readonly string databasePath;

    public OrdersDbContext() : base()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        IConfiguration config = builder.Build();

        databasePath = config.GetConnectionString("DefaultConnection");
    }


    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={databasePath}");
    }

}
