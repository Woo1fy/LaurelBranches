using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LaurelBranches.Data;
using LaurelBranches.Models;

namespace LaurelBranches.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;

        public ProductsController(IProductService productService, IOrderService orderService)
        {
            _productService = productService;
            _orderService = orderService;
        }

        //// GET: Products
        //public async Task<IActionResult> Index()
        //{
        //    var applicationDbContext = _context.Products.Include(p => p.Order);
        //    return View(await applicationDbContext.ToListAsync());
        //}

        //// GET: Products/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var product = await _context.Products
        //        .Include(p => p.Order)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(product);
        //}

        //// GET: Products/Create
        //public IActionResult Create()
        //{
        //    ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id");
        //    return View();
        //}

        // POST: Listings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        

        public async Task<IActionResult> Change(ProductVM product)
        {
            var productObj = new Product
            {
                Title = product.Title,
                Description = product.Description,
                Price = product.Price,
                Discount = product.Discount,
                Weight = product.Weight,
                Composition = product.Composition,
                Calories = product.Calories,
                Proteins = product.Proteins,
                Fats = product.Fats,
                Carbohydrates = product.Carbohydrates
            };

            await _productService.Change(productObj);
            return View(productObj);
        }

        public async Task<IActionResult> Remove(int productId)
        {
            var product = await _productService.GetById(productId);
            await _productService.Remove(product);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct(ProductVM product)
        {
            if (product.Image != null)
            {
                string uploadDir = string.Empty;
                string fileName = product.Image.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    product.Image.CopyTo(fileStream);
                }

                var productObj = new Product
                {
                    Title = product.Title,
                    Description = product.Description,
                    Price = product.Price,
                    Discount = product.Discount,
                    Weight = product.Weight,
                    Composition = product.Composition,
                    Calories = product.Calories,
                    Proteins = product.Proteins,
                    Fats = product.Fats,
                    Carbohydrates = product.Carbohydrates,
                    ImagePath = fileName,
                };
                
                await _productService.Add(productObj);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        //// GET: Products/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var product = await _context.Products.FindAsync(id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", product.OrderId);
        //    return View(product);
        //}

        //// POST: Products/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ImagePath,Description,Price,Discount,Weight,Composition,Calories,Proteins,Fats,Carbohydrates,OrderId")] Product product)
        //{
        //    if (id != product.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(product);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ProductExists(product.Id))
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
        //    ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", product.OrderId);
        //    return View(product);
        //}

        //// GET: Products/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var product = await _context.Products
        //        .Include(p => p.Order)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(product);
        //}

        //// POST: Products/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var product = await _context.Products.FindAsync(id);
        //    if (product != null)
        //    {
        //        _context.Products.Remove(product);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool ProductExists(int id)
        //{
        //    return _context.Products.Any(e => e.Id == id);
        //}
    }
}
