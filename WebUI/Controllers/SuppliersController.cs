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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SupplierViewModel model)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(model),
                System.Text.Encoding.UTF8,
                "application/json"
            );

            await _http.PostAsync(
                "http://localhost:5170/api/suppliers",
                content
            );

            return RedirectToAction("Index");
        }
    }
}
