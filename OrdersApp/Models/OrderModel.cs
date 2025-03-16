using OrdersApp.Entities;
using OrdersApp.Enums;
using System.ComponentModel;
using System.Reflection;

namespace OrdersApp.Models;

public class OrderModel
{
    [DisplayName("Numer")]
    public int Id { get; set; }

    [DisplayName("Nazwa produktu")]
    public string ProductName { get; set; }

    [DisplayName("Cena")]
    public decimal Price { get; set; }

    [DisplayName("Adres dostawy")]
    public string DeliveryAddress { get; set; }

    [DisplayName("Data zamóweinia")]
    [DefaultValue("DateTime.Now")]
    public DateTime OrderDate { get; set; }

    [DisplayName("")]
    public DateTime? SentDate { get; set; }

    [DisplayName("")]
    public DateTime? CanceledDate { get; set; }

    [DisplayName("")]
    public DateTime? ReturnedDate { get; set; }

    [DisplayName("")]
    public DateTime? ClosedDate { get; set; }

    [DisplayName("Status zamówienia")]
    [DefaultValue(OrderStatus.New)]
    public OrderStatus Status { get; set; }

    [DisplayName("Metoda płatności")]
    [DefaultValue(PaymentType.Cash)]
    public PaymentType PaymentType { get; set; }

    [DisplayName("Typ klienta")]
    [DefaultValue(ClientType.Individual)]
    public ClientType ClientType { get; set; }

    public OrderModel() { }
    public OrderModel(Order order)
    {
        Id = order.Id;
        ProductName = order.ProductName;
        Price = order.Price;
        DeliveryAddress = order.DeliveryAddress;
        OrderDate = order.OrderDate;
        SentDate = order.SentDate;
        CanceledDate = order.CanceledDate;
        ReturnedDate = order.ReturnedDate;
        ClosedDate = order.ClosedDate;
        Status = order.Status;
        PaymentType = order.PaymentType;
        ClientType = order.ClientType;
    }

    public Order ToEntity()
    {
        return new Order
        {
            Id = Id,
            ProductName = ProductName,
            Price = Price,
            DeliveryAddress = DeliveryAddress,
            OrderDate = OrderDate,
            SentDate = SentDate,
            CanceledDate = CanceledDate,
            ReturnedDate = ReturnedDate,
            ClosedDate = ClosedDate,
            Status = Status,
            PaymentType = PaymentType,
            ClientType = ClientType
        };
    }

    public static IEnumerable<string> FieldNames()
    {
        return typeof(OrderModel).GetProperties().Where(p => GetDisplayName(p.Name) != string.Empty).Select(p => GetDisplayName(p.Name));

    }

    private static string GetDisplayName(string fieldName)
    {
        var property = typeof(OrderModel).GetProperty(fieldName);
        var displayName = property.GetCustomAttribute<DisplayNameAttribute>();
        return displayName?.DisplayName ?? string.Empty;
    }
}
