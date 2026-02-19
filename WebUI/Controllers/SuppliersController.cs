using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class SuppliersController : Controller
    {
        private readonly HttpClient _http;

        public SuppliersController(IHttpClientFactory factory)
        {
            _http = factory.CreateClient();
        }

        public async Task<IActionResult> Index()
        {
            var json = await _http.GetStringAsync(
                "http://localhost:5170/api/suppliers"
            );

            var suppliers = JsonSerializer.Deserialize<List<SupplierViewModel>>(
                json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            return View(suppliers);
        }


         public async Task<IActionResult> Products(int id)
        {
            var json = await _http.GetStringAsync(
                $"http://localhost:5170/api/suppliers/{id}/products"
            );

            ViewBag.Data = json;
            return View();
        }

    }
}
