using AutoMapper;
using Ecom.API.Rest.Errors;
using Ecom.API.Rest.Helpers;
using Ecom.API.Rest.Middleware;
using Ecom.Apps.Core.Interfaces;
using Ecom.Apps.Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Linq;

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

            services.AddScoped<IProductRepository, ProductRepository>();
            // Generics are registered like below in service container
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddAutoMapper(typeof(MappingProfiles));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ecom.API.Rest", Version = "v1" });
            });


            // Below configuration is just for improving/customizing validation error response
            // Improving means instead of returning dictionary of Validationerrors, we will flatten it out
            // and return list of Validation errors

            // Needs to configure below service for overriding [ApiController] attribute of controllers
            // for improving validation error response

            // This should be below "services.AddControllers()" else we cannot override controller behaviour
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    //  "e" is a key-value pair, we first fetch the value of that key-value pair
                    // then fetch one ICollection prop(called errors) of that "value" , and then count it

                    // Below we are trying to fetch a list/ array of all validation errors
                    // every key is having a collection of errors, and we want to flatten it out means
                    // create a single array of all the errors of all keys, that's why we used selectmany + select
                    var errors = actionContext.ModelState
                                 .Where(e => e.Value.Errors.Count > 0)
                                 .SelectMany(e => e.Value.Errors)
                                 .Select(x => x.ErrorMessage).ToArray();

                    // Add those errors in errorResponse object
                    var errorResponse = new ApiValidationErrorResponse(errors);
                    return new BadRequestObjectResult(errorResponse);
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Right at the top , we add our exception handling middleware and also comment out DeveloperExPage
            // else that middleware will run instead of our custom exception handling middleware
            
            app.UseMiddleware<ExceptionMiddleware>(); // Handling exception or Internal server error

            if (env.IsDevelopment())
            {
                // app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ecom.API.Rest v1"));
            }

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



            // All are middleware and their ordering matters
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
