using Sg.Fc.Portfolio.Stocks.Common.Domain;
using Sg.Fc.Portfolio.Stocks.Datasource;

namespace Sg.Fc.Portfolio.Stocks.Api.Services
{
    public class OrderService : IOrderService
    {
        private readonly IDataSource _dataSource;

        public OrderService(IDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        public List<Order> GetOrders()
        {
            return _dataSource.GetOrders();

        }

        public bool AddOrder(Order order)
        {
            return _dataSource.AddOrder(order);

        }

        public bool DeleteOrder(Guid guid)
        {
            return _dataSource.DeleteOrder(guid);

        }

        public List<Holdings> GetHoldings()
        {
            var orders = _dataSource.GetOrders();

            var transactions = _dataSource.GetTranscations().Where(o => o.SellOrderId == null);
            return orders.Where(o => transactions.Any(x => x.BuyOrderId == o.Id))
            .GroupBy(o => o.Symbol)
            .Select(cl => new Holdings
            {
                Symbol = cl.First().Symbol,
                Quantity = cl.Sum(c => c.Quantity),
                Price = cl.Average(c => c.Price),
                Count = cl.Count(),
                Cost = cl.Sum(c => Math.Round(c.Quantity * c.Price, 2)),

            }).ToList();
        }
    }
}
