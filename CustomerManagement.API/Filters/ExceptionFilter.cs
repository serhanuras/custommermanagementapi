using CustomerManagement.API.Dtos;
using CustomerManagement.API.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CustomerManagement.API.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger _logging;

        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            this._logging = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            _logging.LogError(context.Exception, context.Exception.Message);

            if (context.Exception.GetType() == typeof(NotFoundException))
            {
                context.HttpContext.Response.StatusCode = (int) System.Net.HttpStatusCode.NotFound;
                
                context.Result = new ObjectResult(new ErrorDto()
                {
                    Message = context.Exception.Message

                });
            }
            else if (context.Exception.GetType() == typeof(ConflictException))
            {
                context.HttpContext.Response.StatusCode = (int) System.Net.HttpStatusCode.NotFound;
                
                context.Result = new ObjectResult(new ErrorDto()
                {
                    Message = context.Exception.Message

                });
            }
            else
            {
                context.HttpContext.Response.StatusCode = (int) System.Net.HttpStatusCode.InternalServerError;

                context.Result = new ObjectResult(new ErrorDto()
                {
                    Message = "There is a problem on server side. Please try again later..."

                });
            }

            context.ExceptionHandled = true;
            base.OnException(context);
        }


    }
}