using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly HttpClient _http;

        public ProductsController(IHttpClientFactory factory)
        {
            _http = factory.CreateClient();
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";
            
            var products = await _http.GetFromJsonAsync<List<ProductWithStock>>(
                "http://localhost:5170/api/products"
            );
            
            products = products.OrderBy(p => p.Id).ToList(); // ← сортировка
            
            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            await _http.PostAsJsonAsync(
                "http://localhost:5170/api/products",
                product
            );

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var products = await _http.GetFromJsonAsync<List<Product>>(
                "http://localhost:5170/api/products"
            );

            var product = products.FirstOrDefault(p => p.Id == id);
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            await _http.PutAsJsonAsync(
                $"http://localhost:5170/api/products/{id}",
                product
            );

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _http.DeleteAsync(
                $"http://localhost:5170/api/products/{id}"
            );

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStock(int id, int quantity)
        {
            // 1. Обновляем остаток в StockItem
            await _http.PutAsJsonAsync(
                $"http://localhost:5170/api/stock/{id}",
                new { quantity = quantity }
            );
            
            // 2. Сначала получаем текущий товар, чтобы не потерять Name и Sku
            var product = await _http.GetFromJsonAsync<Product>(
                $"http://localhost:5046/api/products/{id}"
            );
            
            // 3. Обновляем только Quantity, сохраняя остальные поля
            product.Quantity = quantity;
            
            await _http.PutAsJsonAsync(
                $"http://localhost:5046/api/products/{id}",
                product
            );

            return RedirectToAction("Index");
        }
    }
}