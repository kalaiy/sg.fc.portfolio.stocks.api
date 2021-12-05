using Sg.Fc.Portfolio.Stocks.Common.Domain;


namespace Sg.Fc.Alphavantage.Sqprovider.Interfaces
{
    public interface IStockQuoteProvider
    {
        List<StockSymbol> GetSymbol(string keywords);

        Ohlc? GetOhlc(string json, string date);

    }
}