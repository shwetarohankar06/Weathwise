using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using WealthWise.Models;
using WealthWiseAPI.Data;

namespace WealthWise.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Order> PlaceOrder(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Order> GetOrderById(long orderId)
        {
            return await _context.Orders.Include(o => o.OrderItem).FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task<List<Order>> GetAllOrdersForUser(long userId)
        {
            return await _context.Orders.Where(o => o.UserId == userId).Include(o => o.OrderItem).ToListAsync();
        }

        public async Task<bool> CancelOrder(long orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null || order.Status == "COMPLETED") return false;

            order.Status = "CANCELED";
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExecuteTrade(long orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null || order.Status != "PENDING") return false;

            order.Status = "COMPLETED";
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
