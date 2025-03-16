using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrdersApp.Controlers;
using OrdersApp.Data;
using OrdersApp.Enums;
using OrdersApp.Extensions;
using OrdersApp.Services;
using OrdersApp.View;

HostApplicationBuilder hostBuilder = Host.CreateApplicationBuilder(args);
hostBuilder.Services.AddSingleton<OrdersDbContext>();
hostBuilder.Services.AddSingleton<IOrderService, OrderService>();
hostBuilder.Services.AddSingleton<IOrderController, OrderController>();
hostBuilder.Services.AddSingleton<Display>();
using IHost host = hostBuilder.Build();


MainLoop(host.Services.GetRequiredService<IOrderController>(), host.Services.GetRequiredService<Display>());


async void MainLoop(IOrderController orderController, Display mainDisplay)
{
    mainDisplay.Welcome();

    var runApplication = true;
    do
    {
        var keyinfo = Console.ReadKey();
        string testKey = keyinfo.Key == ConsoleKey.Escape ? "ESC" : keyinfo.KeyChar.ToString();
        var command = testKey.GetEnumFromDescription<Commands>();
        switch (command)
        {
            case Commands.NewOrder:
                runApplication = await orderController.New();
                if (runApplication) mainDisplay.Welcome();
                break;
            case Commands.SendToWarehouse:
                runApplication = await orderController.ChangeStatus(OrderStatus.New);
                if (runApplication) mainDisplay.Welcome();
                break;
            case Commands.SendToShipping:
                runApplication = await orderController.ChangeStatus(OrderStatus.Inwarehouse);
                if (runApplication) mainDisplay.Welcome();
                break;
            case Commands.DisplayOrders:
                orderController.Show();
                break;
            case Commands.Exit:
                runApplication = mainDisplay.CloseApplication();
                if (runApplication) mainDisplay.Welcome();
                break;
            default:
                mainDisplay.Welcome();
                break;
        }
    }
    while (runApplication);
}
