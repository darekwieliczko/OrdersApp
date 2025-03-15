using OrdersApp.Attributes;
using System.ComponentModel.DataAnnotations;

namespace OrdersApp.Enums;

public enum PaymentType
{
    [Display(Name = "Gotówka przy odbiorze")]
    [StringValue("G")]
    Cash,

    [Display(Name = "Karta kredytowa")]
    [StringValue("K")]
    Card,

    [Display(Name = "Przelew")]
    [StringValue("P")]
    Transfer
}
