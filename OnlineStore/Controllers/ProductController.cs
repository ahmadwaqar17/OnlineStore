using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using OnlineStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http; // For session handling
using System.Threading.Tasks;

namespace OnlineStore.Controllers
{
    [Authorize] // Apply authentication to the entire controller
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.ToListAsync();
            return View(products);
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Pid == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitCart(int productId)
        {
            // Retrieve email from session
            var email = HttpContext.Session.GetString("UserEmail");

            if (string.IsNullOrEmpty(email))
            {
                TempData["ErrorMessage"] = "You must be logged in to add items to the cart.";
                return RedirectToAction("Index", "Login");
            }

            if (ModelState.IsValid)
            {
                var cartItem = new Cart
                {
                    Pid = productId,
                    Email = email
                };

                _context.Carts.Add(cartItem);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View("Details", new { id = productId });
        }
    }
}
