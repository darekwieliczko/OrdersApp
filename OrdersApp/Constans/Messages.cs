
namespace OrdersApp.Constans;

public static class Messages
{
    public const string ERROR_PRODUCTNAME_EMPTY = "Nazwa produktu nie może byc pusta";
    public const string ERROR_PRODUCTNAME_TOOLONG = "Nazwa produktu nie może być dłuższa niż 30 znaków";
    public const string ERROR_PRODUCTNAME_TOOSHORT = "Nazwa produktu musi być dłuższa niż 3 znaki";
    public const string ERROR_ADRESS_EMPTY = "Adres dostawy pusty.";
    public const string ERROR_ADRESS_TOOSHORT = "Adres dostawy musi być dłuższy niż 5 znaków";
    public const string ERROR_ADRESS_TOOLONG = "Adres dostawy nie może być dłuższy niż 50 znaków";
    public const string ERROR_CLIENTTYPE = "Nieprawidłowy typ klienta";
    public const string ERROR_PAYMENT = "Nieprawidłowa metoda płatności";
    public const string ERROR_STATUS = "Nieprawidłowy status zamówienia";
    public const string ERROR_PRICE = "Cena musi byc większa od 0";

    public const string ERROR_ORDER_ADD = "Nie udało sie dodać zamówienia";
    public const string ERROR_ORDER_NOT_FOUND = "Nie znaleziono zamówienia";
    public const string ORDER_ADDED = "Status zamówienia zmieniony pomyślnie!";
}
