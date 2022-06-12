using Ecom.API.Rest.Errors;
using Ecom.Apps.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ecom.API.Rest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuggyController : ControllerBase
    {
        private readonly StoreContext _storeContext;
        private readonly ILogger<BuggyController> _logger;

        public BuggyController(StoreContext storeContext, ILogger<BuggyController> logger)
        {
           _storeContext = storeContext;
            _logger = logger;
        }

        [HttpGet("notfound")]
        public ActionResult GetNotFoundRequest()
        {
            var thing = _storeContext.Products.Find(42);

            if(thing == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok();
        }

        [HttpGet("servererror")]
        public ActionResult GetServerError()
        {
            try
            {
                var thing = _storeContext.Products.Find(42);

                var thingToReturn = thing.ToString(); // since thing will be null, so accessing ToString method
                                                      // gives null ref error, and hence a server error
                return Ok();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
                //when exception is thrown, then only UsedeveloperExceptionPage/ ExceptionMiddleware
                // of startUp class comes into picture, but if you use below code then they won't come in picture

                // return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }

        [HttpGet("badrequest/{id}")]
        public ActionResult GetBadRequestForValidationError(int id)
        {
            // Trying to generate validation type error by sending string instead of int
            // and that is a bad request
            return Ok();
        }


    }
}
