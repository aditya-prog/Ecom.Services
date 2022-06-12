using Ecom.API.Rest.Errors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ecom.API.Rest.Controllers
{
    [Route("errors/{statusCode}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]  // For fixing swagger error(as we haven't assigned any httpMethod to below API),
                                             // so, we want to ignore below API for swagger documentation as this api
                                             // is not exposed externally but will be used internally
                                             // Simply, Ignore APIs of this controller for documentation, that's all
                                             // But still we can make call directly to this api through postman
                                              
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
