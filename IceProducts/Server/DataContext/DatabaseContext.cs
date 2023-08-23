using IceProducts.Server.Entities;
using IceProducts.Server.Extensions;
using Microsoft.EntityFrameworkCore;

namespace IceProducts.Server.DataContext
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users  { get; set; }
        public DbSet<Product> Products { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
                
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().Property(x => x.Image).IsRequired();
            modelBuilder.Entity<Product>().Property(x => x.SmallDescription).IsRequired().HasMaxLength(75);
            modelBuilder.Entity<Product>().Property(x => x.LongDescription).IsRequired();
            modelBuilder.Entity<Product>().Property(x => x.Name).IsRequired();

            modelBuilder.SeedData();
        }
    }
}
