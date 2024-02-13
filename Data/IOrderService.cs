using LaurelBranches.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LaurelBranches.Data
{
    public interface IOrderService
    {
        IQueryable<Order> GetAll();
        Task Add(Order order);
        Task<Order> GetActiveOrder();
        Task Remove(Order order);
        Task<Order> GetById(int? orderId);
        Task AddProduct(Product product);
        Task RemoveProduct(Product product);
    }
}
