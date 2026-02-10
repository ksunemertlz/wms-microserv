using Microsoft.AspNetCore.Mvc;
using ProductService.Models;

namespace ProductService.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private static List<Product> products = new()
        {
            new Product { Id = 1, Name = "Клавиатура", Sku = "KB001", Quantity = 10 },
            new Product { Id = 2, Name = "Мышь", Sku = "MS002", Quantity = 25 }
        };

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(products);
        }

        //Создание нового товара
        [HttpPost]
        public IActionResult Create(Product product)
        {
            product.Id = products.Count + 1;
            products.Add(product);
            return Ok(product);
        }

        //Редактирование товара по id
        [HttpPut("{id}")]
        public IActionResult Update(int id, Product product)
        {
            var existing = products.FirstOrDefault(p => p.Id == id);
            if (existing == null) return NotFound();

            existing.Name = product.Name;
            existing.Sku = product.Sku;
            existing.Quantity = product.Quantity;

            return Ok(existing);
        }

        //Удаление товара по id
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();

            products.Remove(product);
            return Ok();
        }
    }
}
