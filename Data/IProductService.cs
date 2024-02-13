using LaurelBranches.Models;

namespace LaurelBranches.Data
{
    public interface IProductService
    {
        IQueryable<Product> GetAll();
        Task Add(Product order);
        Task Remove(Product product);
        Task Change(Product newProduct);
        Task<Product> GetById(int? orderId);
        Task SaveChanges();
    }
}
