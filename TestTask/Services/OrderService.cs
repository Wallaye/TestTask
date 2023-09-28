using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services
{
    public class OrderService : IOrderService
    {
        private ApplicationDbContext _context;
        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public Task<Order> GetOrder()
        {
            var order = _context.Orders
                .OrderByDescending(o => o.Price * o.Quantity)
                .FirstOrDefaultAsync();
            return order;
        }

        public Task<List<Order>> GetOrders()
        {
            var orders = _context.Orders
                .Where(o => o.Quantity > 10)
                .ToListAsync();
            return orders;
        }
    }
}
