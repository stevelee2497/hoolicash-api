using HooliCash.Shared;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace HooliCash.API.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await next(context);
            }
            catch (Exception exceptionObj)
            {
                await HandleExceptionAsync(context, exceptionObj);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var error = new Error
            {
                Message = exception.Message,
                StackTrace = exception.StackTrace,
                InnerException = exception.InnerException?.Message
            };
            context.Response.StatusCode = exception is HooliCashException ? (int)HttpStatusCode.InternalServerError : (int)HttpStatusCode.BadRequest;
            return context.Response.WriteAsJsonAsync(error);
        }
    }

    public class Error
    {
        public string Message { get; set; }

        public string StackTrace { get; set; }

        public string InnerException { get; set; }
    }
}
