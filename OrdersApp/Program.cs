using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrdersApp;
using OrdersApp.Data;
using OrdersApp.Enums;
using OrdersApp.Extensions;
using OrdersApp.Services;
using System.Configuration;


HostApplicationBuilder hostBuilder = Host.CreateApplicationBuilder(args);
hostBuilder.Services.AddSingleton<OrdersDbContext>();
hostBuilder.Services.AddSingleton<IOrderService, OrderService>();
using IHost host = hostBuilder.Build();
//await host.StartAsync();

var mainDisplay = new Display(host.Services.GetRequiredService<IOrderService>());

mainDisplay.Welcome();
ConsoleKeyInfo keyinfo;
var runApplication = true;
do
{
    keyinfo = Console.ReadKey();
    
    string testKey = keyinfo.Key == ConsoleKey.Escape ? "ESC" : keyinfo.KeyChar.ToString();
    var command = testKey.GetEnumFromDescription<Commands>();    

    switch (command)
    {
        case Commands.NewOrder:
            mainDisplay.NewOrder();
            break;
        case Commands.SendToWarehouse:
            mainDisplay.OrderToWarehouse();
            break;
        case Commands.SendToShipping:
            mainDisplay.SendOrder();
            break;
        case Commands.DisplayOrders:
            mainDisplay.DisplayOrders();
            break;
        case Commands.Exit:
            runApplication = false;
            break;
        default:
            mainDisplay.Welcome();
            break;
    }
}
while (runApplication);

