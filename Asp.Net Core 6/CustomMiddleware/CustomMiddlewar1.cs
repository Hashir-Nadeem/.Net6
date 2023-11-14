using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Asp.Net_Core_6.CustomMiddleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class CustomMiddlewar1
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomMiddlewar1> _logger;
        private static int count = 0;

        public CustomMiddlewar1(RequestDelegate next,ILogger<CustomMiddlewar1>logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            //global exception handling
            try
            {
                count++;
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                await _next(httpContext);
                stopwatch.Stop();
                await httpContext.Response.WriteAsync(String.Format("Request No: {0} and it takes {1} time", count.ToString(), stopwatch.ElapsedMilliseconds));
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
            }

            
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class CustomMiddlewar1Extensions
    {
        public static IApplicationBuilder UseCustomMiddlewar1(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomMiddlewar1>();
        }
    }
}
