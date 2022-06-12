namespace Ecom.API.Rest.Errors
{
    //Class For handling Interal server error exceptions, so that along with status code and message
    // we also send the exception details especially in developer environment
    public class ApiException : ApiResponse
    {
        public ApiException(int statusCode, string message = null, string details = null) : base(statusCode, message)
        {
            Details = details;
        }

        public string Details { get; }
    }
}
