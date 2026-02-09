using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient _httpClient;

        public AccountController(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var response = await _httpClient.PostAsJsonAsync(
                "http://localhost:5170/api/auth/login",
                new { Username = username, Password = password }
            );

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();

                HttpContext.Session.SetString("username", username);
                HttpContext.Session.SetString("role", "Admin");

                return RedirectToAction("Index", "Dashboard");
            }

            else
            {
                ViewBag.Error = "Неверный логин или пароль";
            }

            return View();
        }
    }
}
