using ConsoleApp;

class Program
{
    static void Main(string[] args)
    {
        ExchangeRate exchangeRate = new ExchangeRate();

        Console.WriteLine(exchangeRate.GetRateFromCurrency("booba"));;
    }
}