using Ecom.API.Rest.Errors;
using Ecom.Apps.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ecom.API.Rest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuggyController : ControllerBase
    {
        private readonly StoreContext _storeContext;

        public BuggyController(StoreContext storeContext)
        {
           _storeContext = storeContext;
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
            var thing = _storeContext.Products.Find(42);

            var thingToReturn = thing.ToString(); // since thing will be null, so accessing ToString method
                                                  // gives null ref error, and hence a server error
            return Ok();
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
