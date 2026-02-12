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

        // Получить все товары
        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _db.Products.ToList();
            return Ok(products);
        }

        // Получить товар по id
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _db.Products.Find(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        // Создание нового товара
        [HttpPost]
        public IActionResult Create(Product product)
        {
            _db.Products.Add(product);
            _db.SaveChanges();
            return Ok(product);
        }

        // Обновление товара
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
            return Ok(existing);
        }

        // Удаление товара
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _db.Products.Find(id);
            if (product == null)
                return NotFound();

            _db.Products.Remove(product);
            _db.SaveChanges();
            return Ok();
        }
    }
}
