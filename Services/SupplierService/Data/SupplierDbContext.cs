using Microsoft.EntityFrameworkCore;
using SupplierService.Models;

namespace SupplierService.Data
{
    public class SupplierDbContext : DbContext
    {
        public SupplierDbContext(DbContextOptions<SupplierDbContext> options)
            : base(options)
        {
        }

        public DbSet<Supplier> Suppliers { get; set; }

         public DbSet<SupplierProduct> SupplierProducts { get; set; }
    }
}
