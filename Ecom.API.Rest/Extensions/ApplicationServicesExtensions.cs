using Ecom.API.Rest.Errors;
using Ecom.Apps.Core.Interfaces;
using Ecom.Apps.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Ecom.API.Rest.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            // Generics are registered like below in service container
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

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

            return services;
        }
    }
}
