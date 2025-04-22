using ECommerce512.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce512.Data
{
    public class ApplicationDbContext : DbContext
    {

        public DbSet<Category> categories { get; set; }
        public  DbSet<Brand> brands { get; set; }
        public DbSet<Product> products { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
           
            optionsBuilder.UseSqlServer(connectionString: "Data Source =. ; Initial Catalog=ECommerce512; Integrated Security = True; TrustServerCertificate = True");
        }

    }
}
