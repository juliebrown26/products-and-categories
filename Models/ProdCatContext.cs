using Microsoft.EntityFrameworkCore;

namespace productscategories.Models
{
    public class ProdCatContext : DbContext
    {
        public ProdCatContext(DbContextOptions options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categorys { get; set; }

        public DbSet<Association> Associations { get; set; }
    }
}
