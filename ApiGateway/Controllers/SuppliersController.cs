using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace ApiGateway.Controllers
{
    [ApiController]
    [Route("api/suppliers")]
    public class SuppliersController : ControllerBase
    {
        private readonly HttpClient _http;

        public SuppliersController(IHttpClientFactory factory)
        {
            _http = factory.CreateClient();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _http.GetFromJsonAsync<object>(
                "http://localhost:5214/api/suppliers"
            );

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(object supplier)
        {
            var response = await _http.PostAsJsonAsync(
                "http://localhost:5214/api/suppliers",
                supplier
            );

            return StatusCode((int)response.StatusCode);
        }

        [HttpGet("{id}/products")]
        public async Task<IActionResult> GetProducts(int id)
        {
            var result = await _http.GetFromJsonAsync<object>(
                $"http://localhost:5214/api/suppliers/{id}/products"
            );

            return Ok(result);
        }

    }
}
