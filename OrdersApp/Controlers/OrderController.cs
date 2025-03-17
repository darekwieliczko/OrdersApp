using OrdersApp.Constans;
using OrdersApp.Entities;
using OrdersApp.Enums;
using OrdersApp.Extensions;
using OrdersApp.Models;
using OrdersApp.Services;
using OrdersApp.Validator;
using OrdersApp.Views;
using System.Linq.Expressions;

namespace OrdersApp.Controlers;

public class OrderController : IOrderController
{
    private const decimal MAX_CASH_PAYMENT = 2500;
    private const int TIME_TO_SEND = 5000;

    private readonly IOrderService orderService;
    private readonly IDisplay orderDisplay;
    private readonly OrderModelValidator orderValidator;

    public OrderController(IOrderService orderService, IDisplay orderDisplay, OrderModelValidator orderValidator)
    {
        this.orderService = orderService;
        this.orderDisplay = orderDisplay;
        this.orderValidator = orderValidator;
    }
    
    public async Task<bool> New()
    {
        var orderModel = orderDisplay.NewOrder();
        var validationResult = orderValidator.Validate(orderModel);
        if (!validationResult.IsValid)
        {
            IEnumerable<(string error, string propertyName)> errorList = validationResult.Errors.Select(e => (e.ErrorMessage, e.PropertyName) );
            orderDisplay.Error(errorList);
            return true;
        }
        
        var order = orderModel.ToEntity();

        order.OrderDate = DateTime.Now;
        order.Status = order.Price > MAX_CASH_PAYMENT && order.PaymentType == PaymentType.Cash ? OrderStatus.Returned : OrderStatus.New;
 
        var result = await orderService.Add(order);

        if (result == null)
        {
            orderDisplay.Error(Messages.ERROR_ORDER_ADD);
        }
        if (result.Id == 0)
        {
            orderDisplay.Error(Messages.ERROR_ORDER_ADD);
        }

        return true;
    }

    public async Task<bool> Show()
    {
        var quit = false;
        var filterType = "";
        do
        {
            var filterClausule = GetStatusFilter(filterType ?? "");
        
            var orders = orderService.GetAll().Where(filterClausule);
            var orderList =  orders.Select(o => new OrderModel(o));

            filterType = orderDisplay.DisplayOrders(orderList);
            if (string.IsNullOrWhiteSpace(filterType) || filterType == "P")
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
                    orderDisplay.Error(Messages.ERROR_ORDER_NOT_FOUND);
                }
                if (result.Status == status.Next())
                {
                    orderDisplay.Success(Messages.ORDER_ADDED);
                }
                if (result.Status == OrderStatus.Inshipping)
                {
                    Task.Run(() => SendOrder(result));
                }

            }
        } while (!quit);
        return true;
    }

    public void Start()
    {
        MainLoop();
    }

    private async void MainLoop()
    {
        orderDisplay.Welcome();

        var runApplication = true;
        do
        {
            var keyinfo = Console.ReadKey();
            string testKey = keyinfo.Key == ConsoleKey.Escape ? "ESC" : keyinfo.KeyChar.ToString();
            var command = testKey.GetEnumFromDescription<Commands>();
            switch (command)
            {
                case Commands.NewOrder:
                    runApplication = await New();
                    if (runApplication) orderDisplay.Welcome();
                    break;
                case Commands.SendToWarehouse:
                    runApplication = await ChangeStatus(OrderStatus.New);
                    if (runApplication) orderDisplay.Welcome();
                    break;
                case Commands.SendToShipping:
                    runApplication = await ChangeStatus(OrderStatus.Inwarehouse);
                    if (runApplication) orderDisplay.Welcome();
                    break;
                case Commands.DisplayOrders:
                    Show();
                    break;
                case Commands.Exit:
                    runApplication = orderDisplay.CloseApplication();
                    if (runApplication) orderDisplay.Welcome();
                    break;
                default:
                    orderDisplay.Welcome();
                    break;
            }
        }
        while (runApplication);
    }


    private async Task SendOrder(Order order)
    {
        await Task.Delay(TIME_TO_SEND);
  
        order.SentDate = DateTime.Now;
        order.Status = OrderStatus.Sent;
        var result = await orderService.Update(order);
    }

}
