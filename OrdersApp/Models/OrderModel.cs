using OrdersApp.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersApp.Models
{
    public class OrderModel
    {
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

        [DisplayName("Data wysyłki")]
        public DateTime? SentDate { get; set; }

        [DisplayName("Data anulowania")]
        public DateTime? CanceledDate { get; set; }

        [DisplayName("Data zwrotu")]
        public DateTime? ReturnedDate { get; set; }

        [DisplayName("Data zamknięcia")]
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


    }
}
