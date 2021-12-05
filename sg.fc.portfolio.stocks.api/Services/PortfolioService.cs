using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Sg.Fc.Alphavantage.Sqprovider.Interfaces;
using Sg.Fc.Portfolio.Stocks.Api.Entity;
using Sg.Fc.Portfolio.Stocks.Common.Domain;


namespace Sg.Fc.Portfolio.Stocks.Api.Services
{

    public class PortfolioService
    {
        private readonly HttpClient _httpClient;
        private readonly IStockQuoteProvider _stockQuoteProvider;
        private readonly APIEndPointOptions _options;


        public PortfolioService(HttpClient httpClient, IOptions<APIEndPointOptions> options, IStockQuoteProvider stockQuoteProvider)
        {
            _httpClient = httpClient;
            _options = options.Value;
            _stockQuoteProvider = stockQuoteProvider;
            _httpClient.BaseAddress = new Uri(_options.BaseUrl);


            _httpClient.DefaultRequestHeaders.Add(
                HeaderNames.UserAgent, "FarallonClient"); // DOS arrest may reject the request if no user agent is present
        }

        public async Task<List<StockSymbol>> GetStockSymbols(string keywords)
        {
            var response = await _httpClient.GetStringAsync($"?{_options.Key}&{String.Format(_options.SymbolSearch, keywords)}");
            return _stockQuoteProvider.GetSymbol(response);
        }

        public async Task<Ohlc?> GetLTP(string symbol, string date)
        {
            var response = await _httpClient.GetStringAsync($"?{_options.Key}&{String.Format(_options.Daily, symbol)}");
            var ohlc = _stockQuoteProvider.GetOhlc(response, date);
            return ohlc;
        }


    }
}
