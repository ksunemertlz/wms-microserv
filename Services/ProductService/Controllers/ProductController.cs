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
    }
}
