using OrdersApp.Attributes;
using System.ComponentModel.DataAnnotations;

namespace OrdersApp.Enums;

public enum ClientType
{
    [Display(Name = "Osoba fizyczna")]
    [StringValue("O")]
    Individual,

    [Display(Name = "Firma")]
    [StringValue("F")]
    Company
}
