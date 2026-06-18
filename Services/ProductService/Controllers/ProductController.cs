using Microsoft.AspNetCore.Mvc;
using ProductService.Models;
using ProductService.Data;
using Microsoft.EntityFrameworkCore;

namespace ProductService.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly ProductDbContext _db;

        public ProductController(ProductDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _db.Products.ToList();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _db.Products.Find(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            _db.Products.Add(product);
            _db.SaveChanges();

            _db.AuditLogs.Add(new AuditLog
            {
                Username = "admin",
                Action = "Создан товар",
                Entity = $"Product #{product.Id}",
                Timestamp = DateTime.UtcNow
            });

            _db.SaveChanges();

            return Ok(product);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Product product)
        {
            var existing = _db.Products.Find(id);
            if (existing == null)
                return NotFound();

            existing.Name = product.Name;
            existing.Sku = product.Sku;
            existing.Quantity = product.Quantity;

            _db.SaveChanges();

            _db.AuditLogs.Add(new AuditLog
            {
                Username = "admin",
                Action = "Обновлен товар",
                Entity = $"Product #{existing.Id}",
                Timestamp = DateTime.UtcNow
            });

            _db.SaveChanges();

            return Ok(existing);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _db.Products.Find(id);
            if (product == null)
                return NotFound();

            _db.Products.Remove(product);
            _db.SaveChanges();

            _db.AuditLogs.Add(new AuditLog
            {
                Username = "admin",
                Action = "Удален товар",
                Entity = $"Product #{product.Id}",
                Timestamp = DateTime.UtcNow
            });

            _db.SaveChanges();

            return Ok();
        }

        [HttpGet("audit")]
        public IActionResult GetAuditLogs()
        {
            var logs = _db.AuditLogs
                .OrderByDescending(x => x.Timestamp)
                .ToList();

            return Ok(logs);
        }
    }
}
