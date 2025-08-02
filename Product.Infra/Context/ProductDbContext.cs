using Microsoft.EntityFrameworkCore;

namespace Product.Infra.Context
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options) { } 
        
        public DbSet<Domain.Product> Products { get; set; }
        public DbSet<Domain.User> Users { get; set; }
    }
}
