using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Asp.Net_Core_6.CustomMiddleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class CustomMiddleware1
    {
        private readonly RequestDelegate _next;
        public static int count = 0;

        public CustomMiddleware1(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            count++;
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            await _next(httpContext);
            stopwatch.Stop();
            await httpContext.Response.WriteAsync(String.Format("Request No: {0} and it takes {1} time",count.ToString(),stopwatch.ElapsedMilliseconds));
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class CustomMiddleware1Extensions
    {
        public static IApplicationBuilder UseCustomMiddleware1(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomMiddleware1>();
        }
    }
}
