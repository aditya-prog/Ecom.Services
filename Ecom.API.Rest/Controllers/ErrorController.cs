using Ecom.API.Rest.Errors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ecom.API.Rest.Controllers
{
    [Route("errors/{statusCode}")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        // Below method is executed when requested endPoint is not found and our middleware
        // ( app.UseStatusCodePagesWithReExecute("/errors/{0}");) redirects us to this controller 
        public IActionResult Error(int statusCode)
        {
            return new ObjectResult(new ApiResponse(statusCode, "Requested End Point doesn't exists"));
        }
    }
}
