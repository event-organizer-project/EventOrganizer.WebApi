using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace EventOrganizer.WebApi.Filters
{
    public class InternalServerErrorExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly ILogger<InternalServerErrorExceptionFilterAttribute> logger;

        public InternalServerErrorExceptionFilterAttribute(ILogger<InternalServerErrorExceptionFilterAttribute> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public override void OnException(ExceptionContext context)
        {
            logger.LogError(context.Exception, "An unhandled exception occurred");

            context.Result = new ObjectResult("An unexpected error occurred")
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };
            context.ExceptionHandled = true;
        }
    }
}
