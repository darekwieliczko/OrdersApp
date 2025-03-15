using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OrdersApp.Enums;

public enum Commands
{
    [Description("1")]
    [Display(Name = "[fuchsia]Nowe zamówienie[/]")]
    NewOrder,

    [Description("2")]
    [Display(Name = "[dodgerblue1]Przekazanie zamówienia do magazynu[/]")]
    SendToWarehouse,

    [Description("3")]
    [Display(Name = "[deepskyblue4_2]Przekazanie zamówienia do wysyłki[/]")]
    SendToShipping,

    [Description("4")]
    [Display(Name = "[dodgerblue2]Przegląd zamówień[/]")]
    DisplayOrders,

    [Description("ESC")]
    [Display(Name = "[aqua]Koniec pracy[/]")]
    Exit
}
