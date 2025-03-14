using Microsoft.OpenApi.Extensions;
using OrdersApp.Enums;
using OrdersApp.Extensions;
using Spectre.Console;


namespace OrdersApp;

public static class Display
{
    private static readonly string ApplicationTitle = "[lime]Aplikacja do obsługi zamówień[/]";

    private static readonly IDictionary<Commands, string> ApplicationMenu = new Dictionary<Commands, string>
    {
        { Commands.NewOrder,        "[fuchsia]Nowe zamówienie[/]" },
        { Commands.SendToWarehouse, "[dodgerblue1]Przekazanie zamówienia do magazynu[/]" },
        { Commands.SendToShipping,  "[deepskyblue4_2]Przekazanie zamówienia do wysyłki[/]" },
        { Commands.DisplayOrders,   "[dodgerblue2]Przegląd zamówień[/]" },
        { Commands.Exit,            "[aqua]Wyjście[/]" }
    };

    private static readonly int LineWidth = 50;

    public static void Welcome()
    {
        Header();
        ShowCommands();
        Footer();
    }

    public static void NewOrder()
    {
        Header();

        Footer();
    }
    public static void OrderToWarehouse()
    {
        Header();

        Footer();
    }

    public static void SendOrder()
    {
        Header();

        Footer();
    }

    public static void DisplayOrders()
    {
        Header();
        Footer();
    }

    private static void Header()
    {
        AnsiConsole.Clear();
        var panelTitle = new Panel(ApplicationTitle).DoubleBorder();
        panelTitle.Width = LineWidth;
        AnsiConsole.Write(panelTitle);
    }
    private static void Footer()
    {
        var panelFooter = new Panel("[bold][italic]Wybierz polecenie?[/][/]").HeavyBorder();
        panelFooter.Width = LineWidth;

        AnsiConsole.Write(panelFooter);
    }

    private static void ShowCommands()
    {
        var table = new Table().HideHeaders().HideFooters().AddColumns("", "");
        table.Width = LineWidth;
        foreach (var item in ApplicationMenu)
        {
            var command = item.Key.GetDescription() != string.Empty ? item.Key.GetDescription() : ((int)item.Key).ToString();
            table.AddRow(command, item.Value);
        }
        AnsiConsole.Write(table);
    }

    public static void Error(string message)
    {
        AnsiConsole.MarkupLine($"[red]{message}[/]");
    }

}
