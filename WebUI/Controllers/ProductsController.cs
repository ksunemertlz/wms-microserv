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
                var products = await _http.GetFromJsonAsync<List<Product>>(
                "http://localhost:5170/api/products"
            );

            return View(products);
        }
    }
}
