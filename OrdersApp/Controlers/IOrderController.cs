using OrdersApp.Enums;

namespace OrdersApp.Controlers;

public interface IOrderController
{
    Task<bool> New();
    Task<bool> ChangeStatus(OrderStatus newStatus = OrderStatus.New);
    Task<bool> Show();
    void Start();
}
