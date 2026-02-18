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

        public OrdersController(OrderDbContext db)
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
            // уменьшаем склад
            var response = await _http.PutAsync(
                $"http://localhost:5261/api/stock/decrease?productId={order.ProductId}&quantity=1",
                null
            );

            if (!response.IsSuccessStatusCode)
                return BadRequest("Не удалось списать товар");

            // сохраняем заказ
            _db.Orders.Add(order);
            _db.SaveChanges();

            return Ok(order);
        }
    }
}
