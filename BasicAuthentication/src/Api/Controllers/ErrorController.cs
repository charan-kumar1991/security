using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Api.Controllers
{
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [Route("/Error")]
        public IActionResult GetExceptionDetails()
        {
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerFeature>();
            Exception e = exceptionDetails.Error;
            string exceptionMessage = e.Message;
            string stackTrace = e.StackTrace;

            return Problem(
                detail: stackTrace,
                statusCode: StatusCodes.Status500InternalServerError,
                title: "Server error",
                instance: exceptionMessage
                );
        }
    }
}
