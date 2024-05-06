

using System.Text.Json;

namespace ConsoleApp;

class Valute
{
    public double Value { get; set; }
    public string Name { get; set; }
    public double Nominal { get; set; }
}

class Root
{
    public Dictionary<string, Valute> Valute { get; set; }
}

public class ExchangeRate
{
    private string _apiUrl;
    private Root _root;

    public ExchangeRate()
    {
        _apiUrl = "https://www.cbr-xml-daily.ru/daily_json.js";
            
        HttpClient client = new HttpClient();

        HttpResponseMessage response = client.GetAsync(_apiUrl).Result;
            
        if (response.IsSuccessStatusCode)
        {
            string json = response.Content.ReadAsStringAsync().Result;

            _root = JsonSerializer.Deserialize<Root>(json);

            /*foreach (var pair in _root.Valute)
            {
                Console.WriteLine($"{pair.Key} - {pair.Value.Value} - {pair.Value.Name} - {pair.Value.Nominal}");
            }*/
        }
        else
        {
            Console.WriteLine($"Ошибка при запросе к API: {response.StatusCode}");
        }
    }

    private bool CurrencyInList(string currency)
    {
        foreach (var cur in _root.Valute.Keys)
        {
            if (cur == currency) return true;
        }

        Console.WriteLine("There is no such currency in the api");
        return false;
    }

    public double GetRateFromCurrency(string currency)
    {
        return CurrencyInList(currency) 
            ? _root.Valute[currency].Value 
            : 0;
    }
        
    public string GetNameFromCurrency(string currency)
    {
        return CurrencyInList(currency) 
            ? _root.Valute[currency].Name 
            : "";
    }
        
    public double GetNominalFromCurrency(string currency)
    {
        return CurrencyInList(currency) 
            ? _root.Valute[currency].Nominal 
            : 0;
    }

    public double ConvertCurrency(string fromCurrency, string toCurrency, double amount)
    {
        if (!CurrencyInList(fromCurrency) || !CurrencyInList(toCurrency)) return 0;

        return amount * GetRateFromCurrency(fromCurrency) / GetRateFromCurrency(toCurrency);
    }
}