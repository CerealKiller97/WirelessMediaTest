using DataAccess.Configurations;
using DataAccess.Extensions;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAccess
{
    public class WirlessDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }

        public WirlessDbContext(DbContextOptions<WirlessDbContext> options): base(options)
        {
            
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new VendorConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ManufacturerConfiguration());

            // TODO: Seed
            modelBuilder.Seed();
        }
    }
}