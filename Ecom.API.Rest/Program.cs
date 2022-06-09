using Ecom.Apps.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Ecom.API.Rest
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // It seems we cannot inject storeContext direcly in Program class ctor, becoz when this ctor
            // runs, then even startup class is not initialized, so storeContext service is not registerd
            // at that time

            var host = CreateHostBuilder(args).Build();
            // Becoz we are outside of services conatiner of statrtUp class, where we don't have control
            // over the lifetime of the context, we will use using method so that dbContext destroys 
            // as soon as we are done

            // Below code is for applying Migration(which were unapplied) automatically on database,
            // when we run the application  (F5)
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                try
                {
                    var context = services.GetRequiredService<StoreContext>();
                    // for running pending migrations
                    await context.Database.MigrateAsync();

                    // for populating tables, if empty
                    await StoreContextSeed.SeedAsync(context, loggerFactory);
                }
                catch(Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(ex, "Error occured during migration in Main method");

                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
