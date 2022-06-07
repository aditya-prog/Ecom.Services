using Ecom.API.Rest.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Ecom.API.Rest.Data
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
