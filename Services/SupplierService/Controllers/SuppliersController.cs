using Microsoft.AspNetCore.Mvc;
using SupplierService.Data;
using SupplierService.Models;

namespace SupplierService.Controllers
{
    [ApiController]
    [Route("api/suppliers")]
    public class SuppliersController : ControllerBase
    {
        private readonly SupplierDbContext _db;

        public SuppliersController(SupplierDbContext db)
        {
            _db = db;
        }

        // Получить всех поставщиков
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_db.Suppliers.ToList());
        }

        // Создать поставщика
        [HttpPost]
        public IActionResult Create(Supplier supplier)
        {
            _db.Suppliers.Add(supplier);
            _db.SaveChanges();
            return Ok(supplier);
        }

        [HttpGet("{id}/products")]
        public async Task<IActionResult> GetSupplierProducts(int id)
        {
            var links = _db.SupplierProducts
                        .Where(x => x.SupplierId == id)
                        .ToList();

            var http = new HttpClient();

            var products = new List<object>();

            foreach (var link in links)
            {
                var product = await http.GetFromJsonAsync<object>(
                    $"http://localhost:5046/api/products/{link.ProductId}"
                );

                products.Add(product);
            }

            return Ok(products);
        }

    }
}
