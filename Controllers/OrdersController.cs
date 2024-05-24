using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LaurelBranches.Data;
using LaurelBranches.Models;
using Microsoft.AspNetCore.Hosting;

namespace LaurelBranches.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;

        public OrdersController(IOrderService orderService, IProductService productService)
        {
            _orderService = orderService;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _orderService.GetAll();
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> Remove(int orderId)
        {
            var order = await _orderService.GetById(orderId);
            await _orderService.Remove(order);
            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderVM order)
        {
            var orderObj = new Order
            {
                Title = order.Title,
                Description = order.Description,
                Active = order.Active,
                Deliverable = order.Deliverable,
                DeliveryFee = order.DeliveryFee,
                Amount = order.Amount,
                IdentityUserId = order.IdentityUserId,
                Promotional = order.Promotional
            };
            await _orderService.Add(orderObj);

            var product = await _productService.GetById(6);
            product.OrderId = orderObj.Id;
            await _productService.SaveChanges();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AddToOrder(int productId, int count)
        {
            Product product = await _productService.GetById(productId);
            for (int i = 0; i < count; i++)
            {
                await _orderService.AddProduct(product);
            }

            return View(product);
        }

        public async Task<IActionResult> RemoveFromOrder(int productId, int count)
        {
            Product product = await _productService.GetById(productId);
            for (int i = 0; i < count; i++)
            {
                await _orderService.RemoveProduct(product);
            }

            return View(product);
        }
    }
}
