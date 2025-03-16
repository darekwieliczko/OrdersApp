using OrdersApp.Attributes;
using System.ComponentModel.DataAnnotations;

namespace OrdersApp.Enums;

public enum OrderStatus
{
    [Display(Name = "Nowe")]
    [StringValue("N")]
    New,

    [Display(Name = "W magazynie")]
    [StringValue("M")]
    Inwarehouse,

    [Display(Name = "W wysyłce")]
    [StringValue("I")]
    Inshipping,

    [Display(Name = "Wysłane")]
    [StringValue("W")]
    Sent,

    [Display(Name = "Anulowane")]
    [StringValue("A")]
    Canceled,

    [Display(Name = "Zwrócono")]
    [StringValue("R")]
    Returned,

    [Display(Name = "Błąd")]
    [StringValue("B")]
    Error,
    
    [Display(Name = "Zamknięte")]
    [StringValue("Z")]
    Closed
}
