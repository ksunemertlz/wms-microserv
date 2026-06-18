using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using StockService.Data;
using StockService.Models;

namespace StockService.Controllers
{
    //[Authorize]
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

        [HttpGet("low")]
        public IActionResult GetLowStock()
        {
            var lowStock = _db.Stock
                .Where(x => x.Quantity < 5)
                .ToList();

            return Ok(lowStock);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateQuantityRequest request)
        {
            var item = _db.Stock.FirstOrDefault(s => s.Id == id);
            if (item == null) return NotFound();
            
            item.Quantity = request.Quantity;
            _db.SaveChanges();
            
            return Ok(item);
        }

        public class UpdateQuantityRequest
        {
            public int Quantity { get; set; }
        }
    }
}