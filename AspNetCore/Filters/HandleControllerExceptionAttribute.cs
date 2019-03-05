using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AspNetCore.Filters
{
    public class HandleControllerExceptionAttribute: ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            context.Result = new ContentResult
            {
                Content = $"Error: {exception.Message}",
                ContentType = "text/plain",
                // change to whatever status code you want to send out
                StatusCode = (int?)HttpStatusCode.BadRequest 
            };
        }
    }
}