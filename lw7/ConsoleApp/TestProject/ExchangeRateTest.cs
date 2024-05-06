using Moq;
using ConsoleApp;

namespace TestProject;

public interface IExchangeRateService
{
    double GetRateFromCurrency(string currency);
    string GetNameFromCurrency(string currency);
    double GetNominalFromCurrency(string currency);
    double ConvertCurrency(string fromCurrency, string toCurrency, double amount);
}

[TestFixture]
public class ExchangeRateTest
{
    /*private Mock<IExchangeRateService> mock;*/
    [Test]
    public void bebra()
    {
        //arrange
        var stub = new Mock<IExchangeRateService>();
        double expectedResult = 87.5;

        stub.Setup(x => x.GetRateFromCurrency("USD")).Returns(70);
        stub.Setup(x => x.GetRateFromCurrency("EUR")).Returns(80);

        var exchangeRate = new ExchangeRate();
        
        //act
        double result = exchangeRate.ConvertCurrency("USD", "EUR", 100);

        //assert
        Assert.That(expectedResult, Is.EqualTo(result));
    }
}