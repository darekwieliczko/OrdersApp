using OrdersApp.Enums;
using OrdersApp.Models;

namespace OrdersApp.Views;

public interface IDisplay
{
    void Welcome();
    OrderModel NewOrder(OrderModel order = null);
    string ChangeStatus(IEnumerable<OrderModel> orderList, OrderStatus newStatus);
    string DisplayOrders(IEnumerable<OrderModel> orderList);
    bool CloseApplication();
    void Error(string message);
    void Error(IEnumerable<(string error, string propertyName)> errorList);
    void Success(string message);
}
