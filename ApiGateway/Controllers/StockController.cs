using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace ApiGateway.Controllers
{
    [ApiController]
    [Route("api/stock")]
    public class StockController : ControllerBase
    {
        private readonly HttpClient _http;

        public StockController(IHttpClientFactory factory)
        {
            _http = factory.CreateClient();
        }

        // Получить все остатки
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _http.GetFromJsonAsync<object>(
                "http://localhost:5261/api/stock"
            );

            return Ok(result);
        }
    }
}
