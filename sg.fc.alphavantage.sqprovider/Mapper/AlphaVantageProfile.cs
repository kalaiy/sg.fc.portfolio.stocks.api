using AutoMapper;
using Newtonsoft.Json.Linq;
using Sg.Fc.Portfolio.Stocks.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sg.Fc.Alphavantage.Sqprovider.Mapper
{
    
    public class AlphaVantageProfile : Profile
    {
        public AlphaVantageProfile()
        {
            // Use CreateMap... Etc.. here (Profile methods are the same as configuration methods)
            CreateMap<JObject, List<StockSymbol>>().ConvertUsing<JObjectToStockSymbolConverter>();
            var symbolMap = CreateMap<JToken, StockSymbol>();
            symbolMap.ForMember(x => x.Symbol, y => y.MapFrom(j => j.SelectToken(".['1. symbol']")));
            symbolMap.ForMember(x => x.Name, y => y.MapFrom(j => j.SelectToken(".['2. name']")));
            symbolMap.ForMember(x => x.Type, y => y.MapFrom(j => j.SelectToken("['3. type']")));
            symbolMap.ForMember(x => x.Region, y => y.MapFrom(j => j.SelectToken("['4. region']")));
            symbolMap.ForMember(x => x.MarketOpen, y => y.MapFrom(j => j.SelectToken("['5. marketOpen']")));
            symbolMap.ForMember(x => x.MarketClose, y => y.MapFrom(j => j.SelectToken("['6. marketClose']")));
            symbolMap.ForMember(x => x.Timezone, y => y.MapFrom(j => j.SelectToken("['7. timezone']")));
            symbolMap.ForMember(x => x.Currency, y => y.MapFrom(j => j.SelectToken("['8. currency']")));
            symbolMap.ForMember(x => x.MatchScore, y => y.MapFrom(j => j.SelectToken("['9. matchScore']")));

            var ohlcMap = CreateMap<JToken, Ohlc>();
            ohlcMap.ForMember(x => x.Open, y => y.MapFrom(j => j.SelectToken(".['1. open']")));
            ohlcMap.ForMember(x => x.High, y => y.MapFrom(j => j.SelectToken(".['2. high']")));
            ohlcMap.ForMember(x => x.Low, y => y.MapFrom(j => j.SelectToken("['3. low']")));
            ohlcMap.ForMember(x => x.Close, y => y.MapFrom(j => j.SelectToken("['4. close']")));
            ohlcMap.ForMember(x => x.Volume, y => y.MapFrom(j => j.SelectToken("['6. volume']")));
            
        }
    }
}
