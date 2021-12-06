using Sg.Fc.Portfolio.Stocks.Common.Domain;

namespace Sg.Fc.Portfolio.Stocks.Api.Services
{
    public interface IOrderService
    {
        bool AddOrder(Order order);
        bool DeleteOrder(Guid guid);
        List<Holdings> GetHoldings();
        List<Order> GetOrders();
    }
}