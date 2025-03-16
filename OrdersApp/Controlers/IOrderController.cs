using OrdersApp.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersApp.Controlers;

public interface IOrderController
{
    Task<bool> New();
    Task<bool> ChangeStatus(OrderStatus newStatus = OrderStatus.New);
    Task<bool> Show();
}
