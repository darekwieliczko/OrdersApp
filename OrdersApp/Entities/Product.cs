using System.ComponentModel;

namespace OrdersApp.Entities;

public class Product
{
    public int Id { get; set; }

    [DisplayName("Nazwa produktu")]
    public string Name { get; set; }

    [DisplayName("Cena produktu")]
    public decimal Price { get; set; }

    [DisplayName("Opis produktu")]
    public string Description { get; set; }

}
