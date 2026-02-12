using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

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
            var stock = await _http.GetFromJsonAsync<object>(
                "http://localhost:5170/api/stock"
            );

            return View(stock);
        }
    }
}
