using Microsoft.AspNetCore.Mvc;
using ApiGateway.Models;

namespace ApiGateway.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public AuthController(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync(
                "http://localhost:5218/api/auth/login",
                request
            );

            var result = await response.Content.ReadAsStringAsync();
            return Content(result, "application/json");
        }
    }
}
