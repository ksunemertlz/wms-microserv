using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class SuppliersController : BaseController
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

        public async Task<IActionResult> Products(int id)
        {
            // Получаем товары поставщика через ApiGateway
            var products = await _http.GetFromJsonAsync<List<Product>>(
                $"http://localhost:5170/api/suppliers/{id}/products"
            );
            
            ViewBag.SupplierId = id;
            return View(products);
        }

        public async Task<IActionResult> Details(int id)
        {
            // Получаем список всех поставщиков
            var suppliers = await _http.GetFromJsonAsync<List<SupplierViewModel>>("http://localhost:5170/api/suppliers");
            var supplier = suppliers?.FirstOrDefault(s => s.Id == id);
            
            if (supplier == null)
                return NotFound();

            // Получаем товары этого поставщика
            var products = await _http.GetFromJsonAsync<List<Product>>($"http://localhost:5170/api/suppliers/{id}/products");

            ViewBag.Products = products ?? new List<Product>();
            return View(supplier);
        }

        // GET: редактирование
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var suppliers = await _http.GetFromJsonAsync<List<SupplierViewModel>>("http://localhost:5170/api/suppliers");
            var supplier = suppliers?.FirstOrDefault(s => s.Id == id);
            if (supplier == null) return NotFound();
            return View(supplier);
        }

        // POST: редактирование
        [HttpPost]
        public async Task<IActionResult> Edit(int id, SupplierViewModel model)
        {
            if (id != model.Id) return BadRequest();
            
            var content = new StringContent(
                JsonSerializer.Serialize(model),
                System.Text.Encoding.UTF8,
                "application/json"
            );
            
            var response = await _http.PutAsync($"http://localhost:5170/api/suppliers/{id}", content);
            
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");
            
            ViewBag.Error = "Ошибка обновления";
            return View(model);
        }

        // GET: удаление (страница подтверждения)
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var suppliers = await _http.GetFromJsonAsync<List<SupplierViewModel>>("http://localhost:5170/api/suppliers");
            var supplier = suppliers?.FirstOrDefault(s => s.Id == id);
            if (supplier == null) return NotFound();
            return View(supplier);
        }

        // POST: удаление
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _http.DeleteAsync($"http://localhost:5170/api/suppliers/{id}");
            return RedirectToAction("Index");
        }
    }
}
