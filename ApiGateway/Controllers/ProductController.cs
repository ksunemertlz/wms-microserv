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

        //Создание нового товара
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] object product)
        {
            var response = await _http.PostAsJsonAsync(
                "http://localhost:5046/api/products",
                product
            );

            var result = await response.Content.ReadAsStringAsync();
            return Content(result, "application/json");
        }

        //Редактирование товара по id
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] object product)
        {
            var response = await _http.PutAsJsonAsync(
                $"http://localhost:5046/api/products/{id}",
                product
            );

            var result = await response.Content.ReadAsStringAsync();
            return Content(result, "application/json");
        }

        //Удаление товара по id
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _http.DeleteAsync(
                $"http://localhost:5046/api/products/{id}"
            );

            return Ok();
        }

    }
}
