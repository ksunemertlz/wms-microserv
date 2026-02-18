using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly HttpClient _http;

        public ProductsController(IHttpClientFactory factory)
        {
            _http = factory.CreateClient();
        }

        public async Task<IActionResult> Index()
        {
            var products = await _http.GetFromJsonAsync<List<ProductWithStock>>(
                "http://localhost:5170/api/products"
            );

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

        //Редактирование товара по id
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

        //Удаление товара по id
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
            await _http.PutAsJsonAsync(
                $"http://localhost:5170/api/stock/{id}",
                new { Quantity = quantity }
            );

            return RedirectToAction("Index");
        }


    }
}
