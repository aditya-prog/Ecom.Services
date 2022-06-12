using System.Collections.Generic;

namespace Ecom.API.Rest.Errors
{

    // This validation error is checked by [ApiController] attribute ,
    // if we  remove it validation error(bad reqest) will not come
    public class ApiValidationErrorResponse : ApiResponse
    {
        public ApiValidationErrorResponse(string[] validationErrors) : base(400, "Bad request!! Parameter validation errors")
        {
            ValidationErrors = validationErrors;
        }
        // List for all the invalid parameters
        // In IEnumerable properties, we can assign an array . Eg: ValidationErrors = string[]{'x', 'y'};
        public IEnumerable<string> ValidationErrors { get; set; }
    }
}
