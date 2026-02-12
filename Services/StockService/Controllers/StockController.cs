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
    }
}
