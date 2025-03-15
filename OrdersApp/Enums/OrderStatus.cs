using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace OrdersApp.Enums;

public enum OrderStatus
{
    [Display(Name = "Nowe")]
    New,

    [Display(Name = "W magazynie")]
    Inwarehouse,

    [Display(Name = "W wysyłce")]
    Inshipping,

    [Display(Name = "Wysłane")]
    Sent,

    [Display(Name = "Anulowane")]
    Canceled,

    [Display(Name = "Zwrócono do klienta")]
    Returned,

    [Display(Name = "Bład")]
    Error,
    
    [Display(Name = "Zamknięte")]
    Closed
}
