using Microsoft.Extensions.Configuration;
using Sg.Fc.Portfolio.Stocks.Common.Domain;
using Sg.Fc.Portfolio.Stocks.Common.Helper;
using System.Xml;
using System.Xml.Serialization;

namespace Sg.Fc.Portfolio.Stocks.Datasource
{
    public class XmlDataSource : IDataSource
    {
        private readonly string orderLocation;
        private readonly string transcationLocation;

        public XmlDataSource(IConfiguration configuration)
        {
            this.orderLocation = configuration["DataSourceOrder"];
            this.transcationLocation = configuration["DataSourceTransaction"];
        }


        public List<Order> GetOrders()
        {
            return XmlHelper.DeserializeToObject<List<Order>>(this.orderLocation).Where(o => !o.IsDeleted).ToList();
        }

        public List<TransactionOrder> GetTranscations()
        {
            return XmlHelper.DeserializeToObject<List<TransactionOrder>>(this.transcationLocation).ToList();
        }
        public bool AddOrder(Order order)
        {
            var orders = GetOrders();
            var existingOrder = orders.Find(o => o.Id == order.Id && o.OrderType == OrderType.BUY);
            if (existingOrder != null)
                throw new InvalidOperationException($"Order already exists with same Id: {order.Id}");
            orders.Add(order);
            XmlHelper.SerializeToXml(orders, this.orderLocation);
            AddToTransaction(orders, order);
            return true;
        }

        public bool DeleteOrder(Guid orderId)
        {
            var orders = GetOrders();
            var existingOrder = orders.Find(o => o.Id == orderId);
            if (existingOrder != null)
                existingOrder.IsDeleted = true;
            else
                throw new InvalidOperationException($"Order not found!, Id: {orderId}");
            XmlHelper.SerializeToXml(orders, this.orderLocation);
            return true;

        }

       

        private void AddToTransaction(List<Order> orders, Order order)
        {
            var transactions = GetTranscations();
            if (order.OrderType == OrderType.BUY)
            {
                transactions.Add(new TransactionOrder() { BuyOrderId = order.Id });
            }
            else
            {
                var existingBuyOrder = orders.Where(o => o.Symbol == order.Symbol && o.OrderType == OrderType.BUY);
                var buyTransactions = transactions.Where(o=>o.SellOrderId==null && existingBuyOrder.Any(x => x.Id == o.BuyOrderId));
                if (buyTransactions != null)
                {
                    foreach (var transaction in buyTransactions)
                    {
                        transaction.SellOrderId = order.Id;
                    }
                   
                }
            }
            XmlHelper.SerializeToXml(transactions, this.transcationLocation);
        }





    }
}