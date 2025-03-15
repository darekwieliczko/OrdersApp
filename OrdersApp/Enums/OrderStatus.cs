using System.ComponentModel;

namespace OrdersApp.Enums;

public enum OrderStatus
{
    [Description("Nowe")]
    New,
    [Description("W magazynie")]
    Inwarehouse,
    [Description("W wysyłce")]
    Inshipping,
    [Description("Wysłane")]
    Sent,
    [Description("Anulowane")]
    Canceled,
    [Description("Zwrócono do klienta")]
    Returned,
    [Description("Bład")]
    Error,
    [Description("Zamknięte")]
    Closed
}
