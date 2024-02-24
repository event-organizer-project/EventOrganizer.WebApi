using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace EventOrganizer.WebApi.Filters
{
    public class SpecificExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly HttpStatusCode httpStatusCode;
        private readonly Type exceptionType;

        public SpecificExceptionFilterAttribute(HttpStatusCode httpStatusCode, Type exceptionType)
        {
            this.httpStatusCode = httpStatusCode;
            this.exceptionType = exceptionType ?? throw new ArgumentNullException(nameof(exceptionType));

            if (IsExceptionTypeInvalid(exceptionType))
                throw new ArgumentException($"Type {exceptionType.Name} does not inherit Exception type.");
        }

        public override void OnException(ExceptionContext context)
        {
            if (context.Exception.GetType() == exceptionType)
            {
                context.Result = new ObjectResult(context.Exception.Message)
                {
                    StatusCode = (int)httpStatusCode
                };

                context.ExceptionHandled = true;
            }
        }

        private static bool IsExceptionTypeInvalid(Type exceptionType)
        {
            var baseType = exceptionType.BaseType;

            while (baseType != null)
            {
                if (baseType == typeof(Exception))
                    return false;

                baseType = baseType.BaseType;
            }

            return true;
        }
    }
}
