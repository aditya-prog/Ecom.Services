using Ecom.API.Rest.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ecom.API.Rest.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // if there is no exception then request move to next stage/middleware, else exception will be caught
                await _next(context);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                // Here we will write code for handling exceptions which results in intenal server error
                // context.Response.StatusCode = (int)HttpStatusCode.InternalServerError; Similar to below
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";

                var response = _env.IsDevelopment() ? new ApiException(StatusCodes.Status500InternalServerError, ex.Message, ex.StackTrace.ToString())
                                               : new ApiException(StatusCodes.Status500InternalServerError, ex.Message);

                var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                // serialize the ApiException class object
                var json = JsonSerializer.Serialize(response, options);
                await context.Response.WriteAsync(json);
                
            }
        }
    }
}
