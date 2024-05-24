using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LaurelBranches.Data;
using LaurelBranches.Models;
using Microsoft.AspNetCore.Hosting;

namespace LaurelBranches.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;

        public ProductsController(IWebHostEnvironment webHostEnvironment, IProductService productService, IOrderService orderService)
        {
            _webHostEnvironment = webHostEnvironment;
            _productService = productService;
            _orderService = orderService;
        }

        // GET: Products/Index
        public IActionResult Index()
        {
            return View();
        }

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

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // GET: Products/Edit
        public IActionResult Edit()
        {
            return View();
        }

        // GET: Products/Details/6
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productService.GetById(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductVM product)
        {
            if (product.Image != null)
            {
                string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
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
                return RedirectToAction("Index", "Home");
            }

            return View(product);
        }
    }
}
