using Microsoft.Extensions.Configuration;
using Sg.Fc.Portfolio.Stocks.Common.Domain;
using Sg.Fc.Portfolio.Stocks.Common.Helper;
using System.Xml;
using System.Xml.Serialization;

namespace Sg.Fc.Portfolio.Stocks.Datasource
{
    public class XmlDataSource : IDataSource
    {
        private readonly string location;

        public XmlDataSource(IConfiguration configuration)
        {
            this.location = configuration["DataSource"];
        }


        public List<Order> GetOrders()
        {
            return XmlHelper.DeserializeToObject<List<Order>>(this.location).Where(o => !o.IsDeleted).ToList();
        }

        public bool AddOrder(Order order)
        {
            var orders = GetOrders();
            var existingOrder = orders.Find(o => o.Id == order.Id);
            if (existingOrder != null)
                throw new InvalidOperationException($"Order already exists with same Id: {order.Id}");
            orders.Add(order);
            XmlHelper.SerializeToXml(orders, this.location);
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
            XmlHelper.SerializeToXml(orders, this.location);
            return true;


        }




    }
}