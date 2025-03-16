using Xunit;
using Moq;
using OrdersApp.Services;
using OrdersApp.View;
using OrdersApp.Entities;
using System.Linq.Expressions;

namespace OrdersApp.Controlers.Tests
{
    public class OrderControllerTests
    {
        [Fact()]
        public void GetStatusFilterTest()
        {
            IOrderService orderService = new Mock<IOrderService>().Object;
            Display display = new Mock<Display>().Object;
            var orderController = new OrderController(orderService, display);

            Expression<Func<Order, bool>> expected = (order) => true;

            var result = orderController.GetStatusFilter("");

            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equivalent(expected, result);
        }

        [Fact()]
        public void GetFailStatusFilterForTest()
        {
            IOrderService orderService = new Mock<IOrderService>().Object;
            Display display = new Mock<Display>().Object;
            var orderController = new OrderController(orderService, display);

            Expression<Func<Order, bool>> expected = (order) => order.Status == Enums.OrderStatus.New;

            var result = orderController.GetStatusFilter("");

            Xunit.Assert.NotNull(result);
            Xunit.Assert.NotStrictEqual(expected, result);
        }
    }
}