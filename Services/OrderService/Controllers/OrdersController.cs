using Microsoft.AspNetCore.Mvc;
using OrderService.Data;
using OrderService.Models;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly OrderDbContext _db;
        private readonly HttpClient _http;

        public OrdersController(OrderDbContext db, IHttpClientFactory factory)
        {
            _db = db;
            _http = factory.CreateClient();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_db.Orders.ToList());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Order order)
        {
            var product = await _http.GetFromJsonAsync<ProductDto>(
                $"http://localhost:5046/api/products/{order.ProductId}");

            if (product == null)
                return NotFound("Product not found");

            if (product.Quantity < order.Quantity)
                return BadRequest("Not enough stock");

            _db.Orders.Add(order);
            await _db.SaveChangesAsync();

            return Ok(order);
        }
    }
}
