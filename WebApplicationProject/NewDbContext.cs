using Microsoft.EntityFrameworkCore;
using System.Formats.Tar;
using WebApplicationProject.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApplicationProject
{
    public class NewDbContext : DbContext
    {

        public DbSet<Customer> Customers { get; set; }
       
        public DbSet<ServiceProfile> ServiceProfiles { get; set; }
        
        public DbSet<TariffPlan> TariffPlans { get; set; }

        public NewDbContext(DbContextOptions<NewDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasOne(customer => customer.ServiceProfile)
                .WithOne(profile => profile.Customer)
                .HasForeignKey<ServiceProfile>(profile=> profile.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TariffPlan>()
                .HasOne(tariff => tariff.ServiceProfile)
                .WithOne(profile => profile.Tariff_Plan)
                .HasForeignKey<ServiceProfile>(profile => profile.TariffPlan)
                .OnDelete(DeleteBehavior.Restrict);
        }



        //public DbSet<Product> Products { get; set; }

        //public DbSet<ProductCategory> ProductCategory { get; set; }

        //public NewDbContext(DbContextOptions<NewDbContext> options) : base (options) {}


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Product>()
        //        .HasOne(product => product.ProdCategory)
        //        .WithOne(productCategory => productCategory.Product)
        //        .HasForeignKey<ProductCategory>(productCategory => productCategory.ProductId)
        //        .OnDelete(DeleteBehavior.Restrict);
        //}
    }
}