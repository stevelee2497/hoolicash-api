using HooliCash.API.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace HooliCash.API.Extensions
{
    public static class MiddlewareExtensions
    {
        public static void UseCustomExceptionHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
