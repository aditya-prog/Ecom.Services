using Ecom.Apps.Core.Entities;
using Microsoft.EntityFrameworkCore;

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
    }
}
