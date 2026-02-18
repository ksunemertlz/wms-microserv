using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using WebUI.Models;

namespace WebUI.Controllers
{
    [ApiController]
    [Route("api/stock")]
    public class StockController : Controller
    {
        private readonly HttpClient _http;

        public StockController(IHttpClientFactory factory)
        {
            _http = factory.CreateClient();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _http.GetStringAsync(
                "http://localhost:5170/api/stock"
            );

            return Content(result, "application/json");
        }
    }
}
