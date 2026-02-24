using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class StockController : Controller
    {
        private readonly HttpClient _http;

        public StockController(IHttpClientFactory factory)
        {
            _http = factory.CreateClient();
        }

        public async Task<IActionResult> Index()
        {
            var json = await _http.GetStringAsync(
                "http://localhost:5261/api/stock"
            );

            var stock = JsonSerializer.Deserialize<List<StockItem>>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );

            return View(stock);
        }
    }
}