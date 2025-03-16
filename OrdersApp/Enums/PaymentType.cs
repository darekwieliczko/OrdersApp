using OrdersApp.Attributes;
using System.ComponentModel.DataAnnotations;

namespace OrdersApp.Enums;

public enum PaymentType
{
    [Display(Name = "Gotówka")]
    [StringValue("G")]
    Cash,

    [Display(Name = "Karta")]
    [StringValue("K")]
    Card,

    [Display(Name = "Przelew")]
    [StringValue("P")]
    Transfer
}
