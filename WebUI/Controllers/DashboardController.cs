using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class DashboardController : BaseController
    {
        private readonly HttpClient _http;

        public DashboardController(IHttpClientFactory factory)
        {
            _http = factory.CreateClient();
        }

        public async Task<IActionResult> Index()
        {
            var username = HttpContext.Session.GetString("username");
            var role = HttpContext.Session.GetString("role");
            
            if (username == null)
                return RedirectToAction("Login", "Account");

            ViewBag.Username = username;
            ViewBag.Role = role;

            var products = await _http.GetFromJsonAsync<List<Product>>("http://localhost:5170/api/products");
            var orders = await _http.GetFromJsonAsync<List<Order>>("http://localhost:5170/api/orders");
            var stock = await _http.GetFromJsonAsync<List<StockItem>>("http://localhost:5170/api/stock");
            
            var lowStockCount = stock?.Count(s => s.Quantity < 5) ?? 0;
            var today = DateTime.Today;
            var todayOrdersCount = orders?.Count(o => o.Date.Date == today) ?? 0;
            
            var model = new DashboardViewModel
            {
                ProductCount = products?.Count ?? 0,
                OrderCount = orders?.Count ?? 0,
                LowStockCount = lowStockCount,
                TodayOrdersCount = todayOrdersCount,
                RecentOrders = orders?.OrderByDescending(o => o.Date).Take(5).ToList() ?? new(),
                LowStockItems = stock?.Where(s => s.Quantity < 5).Select(s => new StockAlert
                {
                    ProductName = products?.FirstOrDefault(p => p.Id == s.ProductId)?.Name ?? "—",
                    Quantity = s.Quantity
                }).ToList() ?? new()
            };

            return View(model);
        }
    }
}