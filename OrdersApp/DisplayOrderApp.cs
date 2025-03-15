﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OrdersApp.Entities;
using OrdersApp.Enums;
using OrdersApp.Extensions;
using OrdersApp.Helpers;
using OrdersApp.Models;
using OrdersApp.Services;
using Spectre.Console;
using System.Globalization;


namespace OrdersApp;

public class Display
{
    private static readonly string ApplicationTitle = "[lime]Aplikacja do obsługi zamówień[/]";
    private static readonly string ApplicationFooter = "[bold][italic]Wybierz polecenie?[/][/]";
    private static readonly int LineWidth = 50;

    private readonly IOrderService orderService;
    private readonly DisplayNameHelper displayNameHelper;

    public Display(IOrderService orderService)
    {
        this.orderService = orderService;
        this.displayNameHelper = new DisplayNameHelper();
    }

    public void Welcome()
    {
        Header();
        ShowCommands();
        Footer();
    }

    public async Task NewOrder()
    {
        Header("Nowe zamówienie");
        Order order = new Order();

        order.ProductName = AnsiConsole.Ask<string>($"{displayNameHelper.GetDisplayName(order, nameof(order.ProductName))}: ");
        order.Price = AnsiConsole.Ask<decimal>($"{displayNameHelper.GetDisplayName(order, nameof(order.Price))}: ");
        order.DeliveryAddress = AnsiConsole.Ask<string>($"{displayNameHelper.GetDisplayName(order, nameof(order.DeliveryAddress))}: ");

        var payment = AnsiConsole.Prompt(
            new TextPrompt<string>($"{displayNameHelper.GetDisplayName(order, nameof(order.PaymentType))} : {PaymentDescription()}")
                .AddChoice(PaymentType.Cash.StringValue())
                .AddChoice(PaymentType.Card.StringValue())
                .AddChoice(PaymentType.Transfer.StringValue())
                .DefaultValue(PaymentType.Cash.StringValue())
                .Culture(CultureInfo.InstalledUICulture));
        order.PaymentType = (PaymentType)payment.GetEnumFromString<PaymentType>();

        var client = AnsiConsole.Prompt(
            new TextPrompt<string>($"{displayNameHelper.GetDisplayName(order, nameof(order.ClientType))} : {ClientDescription()}")
                .AddChoice(ClientType.Company.StringValue())
                .AddChoice(ClientType.Individual.StringValue())
                .DefaultValue(ClientType.Company.StringValue())
                .Culture(CultureInfo.InstalledUICulture));
        order.ClientType = (ClientType)client.GetEnumFromString<ClientType>();
        order.Status = OrderStatus.New;

        order.OrderDate = DateTime.Now;
        var result = await orderService.Add(order);

        Welcome();
    }
    public void OrderToWarehouse()
    {
        Header("Wyślij do magazynu");

        Footer();
    }

    public void SendOrder()
    {
        Header("Wysyłka zamówienia");

        Footer();
    }

    public void DisplayOrders()
    {
        var orders = orderService.GetAll().Result;

        var orderList = orders.Select(o => new OrderModel(o));
        var fields = OrderModel.FieldNames();

        var orderTable = new Spectre.Console.Table();
        orderTable.AddColumns(fields.ToArray());

        var formatColor = "[green]";
        foreach (var order in orderList)
        {
            orderTable.AddRow(GetRowValues(order, formatColor).ToArray());
            formatColor = formatColor == "[green]" ? "[blue]" : "[green]";
        }

        Header("Lista zamówień");
        AnsiConsole.Write(orderTable);
        Footer("Filtrowanie : ");

    }

    private IEnumerable<string> GetRowValues(OrderModel order, string formatColor)
     => new List<string>
        {
            $"{formatColor}{order.Id.ToString()}[/]",
            $"{formatColor}{order.ProductName}[/]",
            $"{formatColor}{order.Price.ToString("C")}[/]",
            $"{formatColor}{order.DeliveryAddress}[/]",
            $"{formatColor}{order.OrderDate.ToShortDateString()}[/]",
            $"{formatColor}{order.Status.Name()}[/]",
            $"{formatColor}{order.PaymentType.Name()}[/]",
            $"{formatColor}{order.ClientType.Name()}[/]"
        };

    public bool CloseApplication()
    {
        Header("[bold][lime]Zamknięcie aplikacji[/][/]");
        var closePrompt = AnsiConsole.Prompt(
            new TextPrompt<bool>("[red]Czy na pewno chcesz zamknąć aplikację?[/]")
                .AddChoice(true)
                .AddChoice(false)
                .DefaultValue(true)
                .WithConverter(choice => choice ? "y" : "n"));
        return !closePrompt;
    }

    private string ClientDescription()
    {
        return $"{ClientType.Company.StringValue()}-{ClientType.Company.Name()}| {ClientType.Individual.StringValue()}-{ClientType.Individual.Name()}";
    }
    private string PaymentDescription()
    {
        return $"{PaymentType.Cash.StringValue()}-{PaymentType.Cash.Name()}| {PaymentType.Card.StringValue()}-{PaymentType.Card.Name()}| {PaymentType.Transfer.StringValue()}-{PaymentType.Transfer.Name()}";
    }

    private void Header(string info = "")
    {
        AnsiConsole.Clear();
        var panelTitle = new Panel(info == string.Empty ? ApplicationTitle : info).DoubleBorder();
        panelTitle.Width = LineWidth;
        AnsiConsole.Write(panelTitle);
    }
    private void Footer(string info = "")
    {
        var panelFooter = new Panel(info == string.Empty ? ApplicationFooter : info).HeavyBorder();
        panelFooter.Width = LineWidth;

        AnsiConsole.Write(panelFooter);
        AnsiConsole.Cursor.Hide();
        AnsiConsole.Cursor.Move(CursorDirection.Up, 25);
    }

    private void ShowCommands()
    {
        var table = new Spectre.Console.Table().HideHeaders().HideFooters().AddColumns("", "");
        table.Width = LineWidth;

        foreach (Commands value in Enum.GetValues(typeof(Commands)))
        {
            table.AddRow(value.GetDescription() ?? "", value.Name());

        }
        AnsiConsole.Write(table);
    }

    public void Error(string message)
    {
        AnsiConsole.MarkupLine($"[red]{message}[/]");
    }

}
