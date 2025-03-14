using Spectre.Console;


namespace OrdersApp;

public static class Display
{
    private static readonly string ApplicationTitle = "Aplikacja do obsługi zamówień";

    private static readonly string[] ApplicationMenu = new []
    {
        "Nowe zamówienie",
        "Przekazanie zamówienia do magazynu",
        "Przekazanie zamówienia do wysyłki",
        "Przegląd zamówień",
        "Wyjście (ESC)"
    };

    private static readonly int LineWidth = 50;

    public static void Welcome()
    {
        var panelTitle = new Panel(ApplicationTitle).DoubleBorder();
        panelTitle.Width = LineWidth;

        var panelFooter = new Panel("Wybierz polecenie?").HeavyBorder();
        panelFooter.Width = LineWidth;

        var table = new Table().HideHeaders().HideFooters().AddColumns("", "");
        table.Width = LineWidth;

        for (int i = 0; i < ApplicationMenu.Count(); i++)
        {
            table.AddRow((i+1).ToString(), ApplicationMenu[i]);
        }

        AnsiConsole.Write(panelTitle);
        AnsiConsole.Write(table);
        AnsiConsole.Write(panelFooter);
    }





}
