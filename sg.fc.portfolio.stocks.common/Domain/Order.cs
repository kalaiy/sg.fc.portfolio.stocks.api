using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sg.Fc.Portfolio.Stocks.Common.Domain
{
    public class Order
    {
        public Guid Id { get; set; }
        public string Symbol { get; set; } = "";
        public OrderType OrderType { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Cost => Math.Round(Price * Quantity, 2);
        public decimal BrokageFee { get; set; }
        public DateTime BuyDateTime { get; set; }
        public DateTime? SellDateTime { get; set; }
        public bool IsDeleted { get; set; }


    }

    public enum OrderType : int
    {
        BUY = 0,
        SELL
    }
}
