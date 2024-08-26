using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using OnlineStore.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Dashboard()
        {
           
            var startDate = DateTime.Now.AddDays(-30);

            
            var salesData = await _context.DeliveryInfos
                .Where(d => d.OrderDate >= startDate)
                .ToListAsync();

            
            var dailySales = salesData
                .GroupBy(d => d.OrderDate.Date)
                .Select(g => new
                {
                    Date = g.Key,
                    TotalSales = g.Sum(d =>
                    {
                        var product = _context.Products.FirstOrDefault(p => p.Pid == d.ProductId);
                        return product?.Price ?? 0;
                    })
                })
                .OrderBy(g => g.Date)
                .ToList();

            ViewBag.DailySales = dailySales;

            return View();
        }

        public IActionResult Fulfillment()
        {
            var deliveryInfos = _context.DeliveryInfos
                .Join(_context.Products,
                      di => di.ProductId,
                      p => p.Pid,
                      (di, p) => new { di, p })
                .GroupBy(dp => dp.di.Email)
                .Select(g => new
                {
                    Email = g.Key,
                    Phone = g.First().di.Phone,
                    City = g.First().di.City,
                    Street = g.First().di.Street,
                    Area = g.First().di.Area,
                    Products = g.Select(dp => dp.p)
                })
                .ToList();

            return View(deliveryInfos);
        }

        public IActionResult PrintInvoice(string email)
        {
            var deliveryInfos = _context.DeliveryInfos
                .Where(di => di.Email == email)
                .Join(_context.Products,
                      di => di.ProductId,
                      p => p.Pid,
                      (di, p) => new { di, p })
                .ToList();

            var totalPrice = deliveryInfos.Sum(dp => dp.p.Price);

            ViewBag.DeliveryInfos = deliveryInfos;
            ViewBag.TotalPrice = totalPrice;

            return View();
        }

        public IActionResult AddProduct()
        {
            return View();
        }

        // AddProduct
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                ViewBag.SuccessMessage = "Product added successfully!";
            }
            return View(product);
        }

        public IActionResult ProductList()
        {
            var products = _context.Products.ToList();
            return View(products);
        }
public IActionResult Edit(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Pid == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product model)
        {
            if (ModelState.IsValid)
            {
                var product = await _context.Products.FindAsync(model.Pid);
                if (product == null)
                {
                    return NotFound();
                }

                product.Name = model.Name;
                product.Description = model.Description;
                product.ImagePath = model.ImagePath;
                product.Category = model.Category;
                product.Price = model.Price;

                _context.Products.Update(product);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Product updated successfully!";
                return RedirectToAction("ProductList");
            }

            return View(model);
        }

        public IActionResult ProcessingCarts()
        {
            var processingCarts = _context.Carts.Where(c => c.OrderStatus == "Processing").ToList();
            return View(processingCarts);
        }
    }
}
