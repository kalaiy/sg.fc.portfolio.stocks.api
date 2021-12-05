using Sg.Fc.Portfolio.Stocks.Common.Domain;

namespace Sg.Fc.Portfolio.Stocks.Datasource
{
    public interface IDataSource
    {
        bool AddOrder(Order order);
        bool DeleteOrder(Guid orderId);
        List<Order> GetOrders();
    }
}