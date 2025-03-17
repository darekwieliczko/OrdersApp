using Xunit;
using Moq;
using OrdersApp.Services;
using OrdersApp.Views;
using OrdersApp.Entities;
using System.Linq.Expressions;
using OrdersApp.Validator;
using OrdersApp.Models;
using OrdersAppTests.Helpers;

namespace OrdersApp.Controlers.Tests;

public class OrderControllerTests
{
    private readonly Mock<IOrderService> orderService;
    private readonly Mock<IDisplay> display;
    private OrderModelValidator validator;

    public OrderControllerTests()
    {
        orderService = new Mock<IOrderService>();
        display = new Mock<IDisplay>();
        validator = new OrderModelValidator();
    }

    [Fact()]
    public async Task ShowShouldReturnTrue()
    {
        var orderList = new List<Order>
        {
            new Order
            {
                ProductName = TestHelper.GetRandomString(25),
                DeliveryAddress = TestHelper.GetRandomString(45),
                Price = TestHelper.GetRandomDecimal(),
                PaymentType = Enums.PaymentType.Transfer,
                ClientType = Enums.ClientType.Company,
                Status = Enums.OrderStatus.New,
                OrderDate = DateTime.Now
            }
        };

        var orderModelList = orderList.Select(o => new OrderModel(o));
        
        display.Setup(d => d.DisplayOrders(orderModelList)).Returns(() => "P");
        Expression<Func<Order, bool>> expected = (order) => true;
       
        orderService.Setup(o => o.GetAll()).Returns(orderList.AsQueryable());

        var orderController = new OrderController(orderService.Object, display.Object, validator);
        var result = await orderController.Show();

        Xunit.Assert.True(result);
    }

    [Fact()]
    public void GetStatusFilterShouldReturnEmptyFilter()
    {
        var orderController = new OrderController(orderService.Object, display.Object, validator);

        Expression<Func<Order, bool>> expected = (order) => true;

        var result = orderController.GetStatusFilter("");

        Xunit.Assert.NotNull(result);
        Xunit.Assert.Equivalent(expected, result);
    }

    [Fact()]
    public void GetFailStatusFilterForShouldReturnNotEmptyFilter()
    {
        var orderController = new OrderController(orderService.Object, display.Object, validator);

        Expression<Func<Order, bool>> expected = (order) => order.Status == Enums.OrderStatus.New;

        var result = orderController.GetStatusFilter("");

        Xunit.Assert.NotNull(result);
        Xunit.Assert.NotStrictEqual(expected, result);
    }

}