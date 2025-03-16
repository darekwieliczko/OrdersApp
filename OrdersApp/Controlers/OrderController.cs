using OrdersApp.Entities;
using OrdersApp.Enums;
using OrdersApp.Extensions;
using OrdersApp.Models;
using OrdersApp.Services;
using OrdersApp.View;
using System.Linq.Expressions;

namespace OrdersApp.Controlers;

public class OrderController : IOrderController
{

    private readonly IOrderService orderService;
    private readonly Display orderDisplay;


    public OrderController(IOrderService orderService, Display orderDisplay)
    {
        this.orderService = orderService;
        this.orderDisplay = orderDisplay;
    }
    
    public async Task<bool> New()
    {
        var orderModel = orderDisplay.NewOrder();

        var order = orderModel.ToEntity();
        order.Status = OrderStatus.New;
        order.OrderDate = DateTime.Now;

        var result = await orderService.Add(order);

        if (result == null)
        {
            orderDisplay.Error("Order not added");
        }
        if (result.Id == 0)
        {
            orderDisplay.Error("Order not added");
        }

        return true;
    }

    public async Task<bool> Show()
    {
        var quit = false;
        var filterType = "";
        do
        {
            var filterClausule = GetStatusFilter(filterType);
        
            var orders = orderService.GetAll().Where(filterClausule);
            var orderList =  orders.Select(o => new OrderModel(o));

            filterType = orderDisplay.DisplayOrders(orderList);
            if (filterType == "P")
            {
                quit = true;
            }

        } while (!quit);
        return true;
    }

    public Expression<Func<Order, bool>> GetStatusFilter(string filterType)
     {
        if (filterType == string.Empty)
        {
            return (order) => true;
        }

        OrderStatus status = filterType.GetEnumFromString<OrderStatus>().Value;

        if (status != null)
        {
            return (order) => order.Status == status;
        }
        else
        {
            return (order) => true;
        }
    }

    public async Task<bool> ChangeStatus(OrderStatus status = OrderStatus.New)
    {
        var quit = false;
        do
        {
            var orders = orderService.GetAll().Where(orderDisplay => orderDisplay.Status == status);
            var orderList = orders.Select(o => new OrderModel(o));

            var orderId = orderDisplay.ChangeStatus(orderList, status.Next());

            if (orderId == "P")
            {
                quit = true;
            }
            else
            {
                var order = await orderService.Get(int.Parse(orderId));
                order.Status = status.Next();
                var result = await orderService.Update(order);
                if (result == null)
                {
                    orderDisplay.Error("Order not found");
                }
                if (result.Status == status.Next())
                {
                    orderDisplay.Success("Status zamówienia zmieniony pomyślnie!");
                }

            }
        } while (!quit);
        return true;
    }
}
