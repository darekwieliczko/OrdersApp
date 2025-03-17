namespace OrdersAppTests.Helpers;

public static class TestHelper
{

    public static string GetRandomString(int length)
    {
        var random = new Random();
        const string chars = "AĄBCĆDEĘFGHIJKLŁMNOÓPQRSŚTUVWXYZŹ 0123456789 aąbcćdeęfghijklłmnoópqrsśtuwxyzżź";
        return new string(Enumerable.Repeat(chars, length)
          .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public static decimal GetRandomDecimal(int max = 1000)
    {
        var random = new Random();
        return random.Next(1, max);
    }


}
