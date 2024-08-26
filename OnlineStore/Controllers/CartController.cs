using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http; // Make sure to include this for session handling
using OnlineStore.Data;
using OnlineStore.Models;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Cart/ShowCart
        public IActionResult ShowCart()
        {
            // Retrieve email from session
            var email = HttpContext.Session.GetString("UserEmail");

            // Pass the email to the view using ViewData
            ViewData["Email"] = email;

            return View();
        }

        // POST: /Cart/ShowCart
        [HttpPost]
        public async Task<IActionResult> ShowCart(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                ModelState.AddModelError("", "Email is required.");
                return View();
            }

            var cartItems = _context.Carts
                .Where(c => c.Email == email)
                .Select(c => new
                {
                    CartId = c.Id,
                    Product = _context.Products.FirstOrDefault(p => p.Pid == c.Pid),
                    OrderStatus = c.OrderStatus
                })
                .ToList();

            return View("CartItems", cartItems);
        }

        // DELETE: /Cart/DeleteItem
        [HttpPost]
        public async Task<IActionResult> DeleteItem(int cartId)
        {
            var cartItem = await _context.Carts.FindAsync(cartId);
            if (cartItem == null)
            {
                return NotFound();
            }

            _context.Carts.Remove(cartItem);
            await _context.SaveChangesAsync();

            return Ok();
        }

        public async Task<IActionResult> Checkout(string email, DeliveryInfo deliveryInfo)
        {
            if (string.IsNullOrEmpty(email))
            {
                ModelState.AddModelError("", "Email is required.");
                return View(deliveryInfo);
            }

            if (ModelState.IsValid)
            {
                
                var carts = _context.Carts
                    .Where(c => c.Email == email && c.OrderStatus == "Processing")
                    .ToList();

                foreach (var cart in carts)
                {
                   
                    var product = _context.Products.FirstOrDefault(p => p.Pid == cart.Pid);
                    if (product != null)
                    {
                        
                        var newDeliveryInfo = new DeliveryInfo
                        {
                            Email = deliveryInfo.Email,
                            Phone = deliveryInfo.Phone,
                            City = deliveryInfo.City,
                            Street = deliveryInfo.Street,
                            Area = deliveryInfo.Area,
                            ProductId = product.Pid, 
                            OrderDate = DateTime.Now 
                        };

                      
                        _context.DeliveryInfos.Add(newDeliveryInfo);
                    }
                }

                
                await _context.SaveChangesAsync();

                
                foreach (var cart in carts)
                {
                    cart.OrderStatus = "CheckedOut";
                }
                _context.Carts.UpdateRange(carts);
                await _context.SaveChangesAsync();

                return RedirectToAction("ShowCart");
            }

            return View(deliveryInfo);
        }
    }
}
