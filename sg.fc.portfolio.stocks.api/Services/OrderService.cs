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
    }
}
