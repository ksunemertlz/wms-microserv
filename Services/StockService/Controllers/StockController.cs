using Microsoft.AspNetCore.Mvc;
using StockService.Data;
using StockService.Models;

namespace StockService.Controllers
{
    [ApiController]
    [Route("api/stock")]
    public class StockController : ControllerBase
    {
        private readonly StockDbContext _db;

        public StockController(StockDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_db.Stock.ToList());
        }

        [HttpPost]
        public IActionResult Create(StockItem item)
        {
            _db.Stock.Add(item);
            _db.SaveChanges();
            return Ok(item);
        }

        [HttpPut("decrease")]
        public IActionResult DecreaseStock(int productId, int quantity)
        {
            var item = _db.Stock.FirstOrDefault(x => x.ProductId == productId);
            if (item == null) return NotFound();

            if (item.Quantity < quantity)
                return BadRequest("Недостаточно товара");

            item.Quantity -= quantity;
            _db.SaveChanges();

            return Ok(item);
        }
    }
}
