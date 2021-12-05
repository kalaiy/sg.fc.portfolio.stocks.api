namespace Sg.Fc.Portfolio.Stocks.Common.Domain
{
    public class Ohlc
    {
        public string Time { get; set; } = "";
        public decimal Open { get; set; }
        public decimal Close { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Percentage => Math.Round(((Open - Close) / Close) * 100, 2);
        public uint Volume { get; set; }
        public override string ToString()
        {
            return $"{Time} {Open} {High} {Low} {Close}".ToString();

        }
    }
}
