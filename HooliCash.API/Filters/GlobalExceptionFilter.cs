using HooliCash.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace HooliCash.API.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            HttpStatusCode statusCode;
            switch (context.Exception)
            {
                case BadRequestException _:
                    statusCode = HttpStatusCode.BadRequest;
                    break;

                case DataNotFoundException _:
                    statusCode = HttpStatusCode.NotFound;
                    break;

                case InternalServerErrorException _:
                    statusCode = HttpStatusCode.InternalServerError;
                    break;

                case HooliCashException _:
                    statusCode = HttpStatusCode.BadRequest;
                    break;

                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    break;
            }

            var error = new Error
            {
                Message = context.Exception.Message,
                StackTrace = context.Exception.StackTrace,
                InnerException = context.Exception.InnerException?.Message
            };

            var result = new ObjectResult(error) { StatusCode = (int)statusCode };
            context.Result = result;
        }
    }

    public class Error
    {
        public string Message { get; set; }

        public string StackTrace { get; set; }

        public string InnerException { get; set; }
    }
}
