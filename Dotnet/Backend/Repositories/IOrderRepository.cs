using System.Collections.Generic;
using System.Threading.Tasks;
using WealthWise.Models;

namespace WealthWise.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> PlaceOrder(Order order);
        Task<Order> GetOrderById(long orderId);
        Task<List<Order>> GetAllOrdersForUser(long userId);
        Task<bool> CancelOrder(long orderId);
        Task<bool> ExecuteTrade(long orderId);
    }
}
