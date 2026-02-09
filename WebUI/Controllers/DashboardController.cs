using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            var username = HttpContext.Session.GetString("username");
            var role = HttpContext.Session.GetString("role");

            if (username == null)
                return RedirectToAction("Login", "Account");

            ViewBag.Username = username;
            ViewBag.Role = role;

            return View();
        }
    }
}
