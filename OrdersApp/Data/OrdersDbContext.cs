using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OrdersApp.Entities;

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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={databasePath}");
    }

}
