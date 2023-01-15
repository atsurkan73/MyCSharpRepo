using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace MVCApplication
{
    public class NewDbContext : DbContext
    {

      //  public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<ProductCategory> ProductCategory { get; set; }

        public NewDbContext(DbContextOptions<NewDbContext> options) : base (options) {}


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne(product => product.ProdCategory)
                .WithOne(productCategory => productCategory.Product)
                .HasForeignKey<ProductCategory>(productCategory => productCategory.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}