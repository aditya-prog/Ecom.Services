using Ecom.Apps.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Ecom.Apps.Infrastructure.Data
{
    // we are abstracting our database away from our code using db context / ORM tool
    // we do  not directly query our db, we use dbContext methods to query db
    public class StoreContext : DbContext
    {
        // DbContextOptions => Initializes a new instance of DBContext class using specificed options
        // (in our case its conn string) for configuring our db

        // Note when we have more than one context then we should specify the context type in <> bracket
        // for DbContextOptions
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }

        // when we create our migration , this is the method responsible for creating that migration
        // so we override this method and will ask it to look for our configurations

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // base is the class we are deriving from i.e DbContext class
            base.OnModelCreating(modelBuilder);
            // Below code will read our entity configurations for generating migrations
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
