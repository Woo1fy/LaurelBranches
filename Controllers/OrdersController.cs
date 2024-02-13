using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LaurelBranches.Data;
using LaurelBranches.Models;

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

        // GET: Orders
        //public async Task<IActionResult> Index()
        //{
        //    var applicationDbContext = _orderService.GetAll();
        //    return View(await applicationDbContext.ToListAsync());
        //}

        // GET: Orders/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var order = await _context.Orders
        //        .Include(o => o.User)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (order == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(order);
        //}

        //// GET: Orders/Create
        //public IActionResult Create()
        //{
        //    ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id");
        //    return View();
        //}

        //// POST: Orders/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Title,Description,IdentityUserId")] Order order)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(order);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", order.IdentityUserId);
        //    return View(order);
        //}

        public async Task<IActionResult> Remove(int orderId)
        {
            var order = await _orderService.GetById(orderId);
            await _orderService.Remove(order);
            return View(order);
        }

        // POST: Listings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
            return RedirectToAction("Index");
            
            //return View(product);
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

        //// GET: Orders/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var order = await _context.Orders.FindAsync(id);
        //    if (order == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", order.IdentityUserId);
        //    return View(order);
        //}

        //// POST: Orders/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,IdentityUserId")] Order order)
        //{
        //    if (id != order.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(order);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!OrderExists(order.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", order.IdentityUserId);
        //    return View(order);
        //}

        //// GET: Orders/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var order = await _context.Orders
        //        .Include(o => o.User)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (order == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(order);
        //}

        //// POST: Orders/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var order = await _context.Orders.FindAsync(id);
        //    if (order != null)
        //    {
        //        _context.Orders.Remove(order);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool OrderExists(int id)
        //{
        //    return _context.Orders.Any(e => e.Id == id);
        //}
    }
}
