using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sg.Fc.Portfolio.Stocks.Common.Domain
{
    public class TransactionOrder
    {
        public Guid BuyOrderId {get; set;}
        public Guid? SellOrderId { get; set; }

    }
}
