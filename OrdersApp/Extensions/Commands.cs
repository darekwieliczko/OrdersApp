using System.ComponentModel;

namespace OrdersApp.Enums;

public enum Commands
{
    None,
    [Description("1")]
    NewOrder,
    [Description("2")]
    SendToWarehouse,
    [Description("3")]
    SendToShipping,
    [Description("4")]
    DisplayOrders,
    [Description("ESC")]
    Exit
}
