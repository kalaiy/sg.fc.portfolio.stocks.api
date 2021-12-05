namespace Sg.Fc.Portfolio.Stocks.Api.Entity
{
    public class APIEndPointOptions
    {
        public const string APIEndPoint = "APIEndPoint";

        public string BaseUrl { get; set; } = String.Empty;

        public string Key { get; set; } = String.Empty;
        public string SymbolSearch { get; set; } = String.Empty;

        public string Daily { get; set; } = String.Empty;

        

    }
}
