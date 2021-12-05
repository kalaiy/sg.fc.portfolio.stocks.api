using AutoMapper;
using Newtonsoft.Json.Linq;
using Sg.Fc.Alphavantage.Sqprovider.Interfaces;
using Sg.Fc.Portfolio.Stocks.Common.Domain;



namespace Sg.Fc.Alphavantage.Sqprovider
{
    public class AVStockQuoteProvider : IStockQuoteProvider
    {
        private readonly IMapper _mapper;
        public AVStockQuoteProvider(IMapper mapper)
        {
            _mapper = mapper;
        }
        public List<StockSymbol> GetSymbol(string json)
        {

            var symbol = _mapper.Map<JObject, List<StockSymbol>>(JObject.Parse(json));
            return symbol;
        }

        public Ohlc? GetOhlc(string json, string date)
        {

            var jobject = JObject.Parse(json);
            var jtoken = jobject.SelectToken($"$..['{date}']");
            if (jtoken != null)
            {
                var ohlc = _mapper.Map<JToken, Ohlc>(jtoken);
                ohlc.Time = date;
                return ohlc;
            }
            else
            {
                return null;
            }
        }


    }
}