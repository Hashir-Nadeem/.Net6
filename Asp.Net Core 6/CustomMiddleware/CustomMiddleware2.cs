namespace Asp.Net_Core_6.CustomMiddleware
{
    public class CustomMiddleware2 : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await context.Response.WriteAsync("CustomMiddleware2\n");
            await next(context);
        }
    }
}
