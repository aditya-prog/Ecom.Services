using AutoMapper;
using Ecom.API.Rest.Helpers;
using Ecom.API.Rest.Middleware;
using Ecom.Apps.Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ecom.API.Rest.Extensions;

namespace Ecom.API.Rest
{
    public class Startup
    {
        // the config files appsettings .... are stored in memory using _configuration field
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
           _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            // Store context is registered as a service , so we can inject it in constructors
            services.AddDbContext<StoreContext>(x =>
                    x.UseSqlite(_configuration.GetConnectionString("DefaultConnection")));
            services.AddAutoMapper(typeof(MappingProfiles));
            
            // Call the extension method for configuring the essential services
            services.AddApplicationServices();
            services.AddSwaggerDocumentation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Right at the top , we add our exception handling middleware and also comment out DeveloperExPage
            // else that middleware will run instead of our custom exception handling middleware
            
            app.UseMiddleware<ExceptionMiddleware>(); // Handling exception or Internal server error
            app.UseSwaggerDocumentation();

            // If any api end point is not found, then re-execute the request pipeline using an alt path
            // {0} is a placeholder for status code
            /**
            returns below response 
            {
                "statusCode": 404,
                "message": "Requested End Point doesn't exists"
            }
             Along with status code 404 - NotFound
            **/
            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            // Below code also behave similar to above and also returns same response object as above
            // but with status code 200 - Ok, which is not right thing becoz we have encountered error 

            // app.UseStatusCodePagesWithRedirects("/errors/{0}");

            app.UseHttpsRedirection();
            app.UseRouting();
            
            // Used so that our api can serve static content like images present in wwwroot folder
            app.UseStaticFiles();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
