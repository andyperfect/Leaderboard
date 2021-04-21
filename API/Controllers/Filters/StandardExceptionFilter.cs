using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Controllers.Filters
{
    public class StandardExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is UnauthorizedAccessException)
            {
                context.Result = new UnauthorizedObjectResult(new {message = context.Exception.Message});
            }
            else
            {
                context.Result = new BadRequestObjectResult(new {message = context.Exception.Message});
            }
        }
    }
}
