using Microsoft.AspNetCore.Mvc;
using SupplierService.Data;
using SupplierService.Models;

namespace SupplierService.Controllers
{
    [ApiController]
    [Route("api/supplier-products")]
    public class SupplierProductsController : ControllerBase
    {
        private readonly SupplierDbContext _db;

        public SupplierProductsController(SupplierDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        public IActionResult AddProductToSupplier(SupplierProduct model)
        {
            _db.SupplierProducts.Add(model);
            _db.SaveChanges();
            return Ok(model);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_db.SupplierProducts.ToList());
        }
    }
}
