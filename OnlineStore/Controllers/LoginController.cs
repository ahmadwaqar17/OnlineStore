using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using OnlineStore.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OnlineStore.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoginController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                var userExists = await _context.UserLogins.AnyAsync(u => u.Email == email);

                if (userExists)
                {
                    // Set user email in session
                    HttpContext.Session.SetString("UserEmail", email);

                    // Create claims and sign in the user
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, email),
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                    return RedirectToAction("Index", "LandingPage");
                }
                else
                {
                    ViewBag.Error = "Email not found.";
                }
            }
            else
            {
                ViewBag.Error = "Please provide an email.";
            }

            return View();
        }

        public async Task<IActionResult> Logout()
        {
            // Sign out the user
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index");
        }
    }
}
