using LaurelBranches.Models;
using Microsoft.EntityFrameworkCore;

namespace LaurelBranches.Data
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Product> GetAll()
        {
            var applicationDbContext = _context.Products;
            return applicationDbContext;
        }

        public async Task<Product> GetById(int? orderId)
        {
            return await _context.Products
                .Include(o => o.Comments)
                .FirstOrDefaultAsync(m => m.Id == orderId);
        }

        public async Task Add(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task Remove(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task Change(Product newProduct)
        {
            var product = _context.Products
                .FirstOrDefault(p => p.Id == newProduct.Id) ;
            product = newProduct;
            await _context.SaveChangesAsync();
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

    }
}
