using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkDbMigration
{
    internal class NewDbContext1 : DbContext
    {
        //protected string ConnectionString =
        //    "Server=localhost\\ATSURKAN-NB\\MSSQLSERVER01; Integrated Security = True; Database = MyDb; TrustServerCrtificate=True";

        protected string ConnectionStringFromCode = new SqlConnectionStringBuilder
        { 
            DataSource = "localhost\\MSSQLSERVER01",
                         InitialCatalog = "MyDbMigration",  
            IntegratedSecurity = true,
            Encrypt = SqlConnectionEncryptOption.Optional,
            //TrustServerCertificate = true  //Encrypt = SqlConnectionEncryptOption.Optional,
        }.ConnectionString;

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        
        public DbSet<UserCategory> UserCategory { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer(ConnectionStringFromCode); //, builder => builder.EnableRetryOnFailure()

        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(user => user.Category)
                .WithOne(userCategory => userCategory.User)
                .HasForeignKey<UserCategory>(UserCategory => UserCategory.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
