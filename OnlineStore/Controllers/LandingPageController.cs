using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using OnlineStore.Models;
using System.Threading.Tasks;

namespace OnlineStore.Controllers
{
    [Authorize] // Ensures that only authenticated users can access the actions in this controller
    public class LandingPageController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LandingPageController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.ToListAsync();
            return View(products);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitCart(int productId)
        {
            var email = HttpContext.Session.GetString("UserEmail");

            if (string.IsNullOrEmpty(email))
            {
                TempData["ErrorMessage"] = "You must be logged in to add items to the cart.";
                return RedirectToAction("Index", "Login");
            }

            var cartItem = new Cart
            {
                Pid = productId,
                Email = email
            };

            _context.Carts.Add(cartItem);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Product successfully added to cart!";

            return RedirectToAction("Index");
        }

        public IActionResult AddToCart(int productId)
        {
            ViewData["ProductId"] = productId;
            return View();
        }
    }
}
