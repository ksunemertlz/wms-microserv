using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace ApiGateway.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly HttpClient _http;

        public OrdersController(IHttpClientFactory factory)
        {
            _http = factory.CreateClient();
        }

        // Получить все остатки
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _http.GetFromJsonAsync<object>(
                "http://localhost:5251/api/orders"
            );

            return Ok(result);
        }
    }
}
