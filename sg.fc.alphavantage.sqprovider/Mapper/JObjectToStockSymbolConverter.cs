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
    public class JObjectToStockSymbolConverter : ITypeConverter<JObject, List<StockSymbol>>
    {
        public List<StockSymbol> Convert(JObject source, List<StockSymbol> destination, ResolutionContext context)
        {

            var userList = new List<StockSymbol>();
            var tokenCountItems = source.SelectTokens("bestMatches").Children().Count();
            for (int i = 0; i < tokenCountItems; i++)
            {
                var token = source.SelectToken($"bestMatches[{i}]");
                var result = new StockSymbol();

                if (token != null)
                {
                    result = context.Mapper.Map<JToken, StockSymbol>(token);
                }
                userList.Add(result);
            }
            return userList;
        }

    }
}
