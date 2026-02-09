using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly HttpClient _http;

        public ProductController(IHttpClientFactory factory)
        {
            _http = factory.CreateClient();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _http.GetAsync("http://localhost:5046/api/products");
            var result = await response.Content.ReadAsStringAsync();
            return Content(result, "application/json");
        }
    }
}
