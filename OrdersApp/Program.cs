using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrdersApp.Controlers;
using OrdersApp.Data;
using OrdersApp.Services;
using OrdersApp.Validator;
using OrdersApp.Views;

HostApplicationBuilder hostBuilder = Host.CreateApplicationBuilder(args);
hostBuilder.Services.AddSingleton<OrdersDbContext>();
hostBuilder.Services.AddScoped<IOrderService, OrderService>();
hostBuilder.Services.AddScoped<IOrderController, OrderController>();
hostBuilder.Services.AddScoped<IDisplay, Display>();
hostBuilder.Services.AddScoped<OrderModelValidator>();
using IHost host = hostBuilder.Build();


var orderController = host.Services.GetRequiredService<IOrderController>();
orderController.Start();

