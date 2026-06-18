using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using System.Text.Json;

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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _http.GetFromJsonAsync<object>(
                "http://localhost:5261/api/stock"
            );
            return Ok(result);
        }

        [HttpGet("low")]
        public async Task<IActionResult> GetLowStock()
        {
            var result = await _http.GetFromJsonAsync<object>(
                "http://localhost:5261/api/stock/low"
            );
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStock(int id, [FromBody] JsonElement request)
        {
            // ВРЕМЕННО: выводим в консоль, что пришло
            Console.WriteLine($"Получен JSON: {request.ToString()}");
            
            // Пробуем найти количество в разных вариантах
            int quantity = 0;
            if (request.TryGetProperty("quantity", out var q1))
                quantity = q1.GetInt32();
            else if (request.TryGetProperty("Quantity", out var q2))
                quantity = q2.GetInt32();
            else if (request.TryGetProperty("stock", out var q3))
                quantity = q3.GetInt32();
            else
            {
                Console.WriteLine("Не найдено поле с количеством!");
                return BadRequest("Не найдено поле quantity или Quantity");
            }
            
            var response = await _http.PutAsJsonAsync(
                $"http://localhost:5261/api/stock/{id}",
                new { quantity = quantity }
            );
            
            var result = await response.Content.ReadAsStringAsync();
            return Ok(result);
        }
    }
}