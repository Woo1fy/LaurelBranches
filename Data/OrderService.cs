using LaurelBranches.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LaurelBranches.Data
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Order> GetActiveOrder()
        {
            return await GetAll()
                .Include(o => o.Products)
                .ThenInclude(o => o.Comments)
                .FirstOrDefaultAsync(m => m.Active == true);
        }

        public async Task Remove(Order order)
        {
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }

        public async Task<Order> GetById(int? orderId)
        {
            return await _context.Orders
                .Include(o => o.User)
                .Include(o => o.Products)
                .ThenInclude(o => o.Comments)
                .FirstOrDefaultAsync(m => m.Id == orderId);
        }

        public IQueryable<Order> GetAll()
        {
            var applicationDbContext
                = _context.Orders.Include(o => o.User);
            return applicationDbContext;
        }

        public async Task AddProduct(Product product)
        {
            var order = await GetActiveOrder();
            order.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveProduct(Product product)
        {
            var order = await GetActiveOrder();
            order.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task Add(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
