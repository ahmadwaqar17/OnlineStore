using Microsoft.AspNetCore.Mvc;

namespace OnlineStore.Controllers
{
    public class RoleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SelectRole(string role)
        {
            if (role == "Customer")
            {
                return RedirectToAction("Index", "Login");
            }
            else if (role == "Admin")
            {
               
                return RedirectToAction("Dashboard", "Admin");
            }

            return View("Index");
        }

        public IActionResult AdminDashboard()
        {
            
            return View();
        }
    }
}
