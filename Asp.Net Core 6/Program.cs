using Asp.Net_Core_6.CustomMiddleware;
using System.IO.Pipelines;
using System.Linq;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<CustomMiddleware2>();
var app = builder.Build();

//app.UseExceptionHandler();
app.UseStaticFiles();
app.UseRouting();
//app.UseAuthentication();
//app.UseAuthorization();


app.Use(async (HttpContext context,RequestDelegate next) =>
{
    await context.Response.WriteAsync("Middleware1\n");
    await next(context);
});

app.UseMiddleware<CustomMiddleware2>();

app.UseCustomMiddleware1();

app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("Middleware2");
    await next(context);
});

//conditional middleware with accessing the query parameters
app.UseWhen
    (
     context => context.Request.Query.ContainsKey("FirstName"),
     app =>
     {
         app.Use(async (HttpContext context,RequestDelegate next) =>
         {
             var temp = context.Request.Query;
             await context.Response.WriteAsync(context.Request.Query["FirstName"]);
         });
     }
    ); ;
//Short Circuiting Middleware
app.Run(async (HttpContext context) =>
{
    await context.Response.WriteAsync("App.Run()");
});

app.UseEndpoints(endpoints =>
{
    //copy request body to file
    endpoints.MapPost("/uploadstream", async (HttpContext context)=>
    {
        var filePath = Path.Combine("D:\\", Path.GetRandomFileName());
        await using var writeStream = File.Create(filePath);
        await context.Request.Body.CopyToAsync(writeStream);
    });
    //copy request body to file but by Body Reader that give us direct access to the body and doesnt buffer the whole data
    endpoints.MapPost("/uploadstreamwriter", async (HttpContext context)=>
    {
        var filePath = Path.Combine("D:\\", Path.GetRandomFileName());
        await using var writeStream = File.Create(filePath);
        await context.Request.BodyReader.CopyToAsync(writeStream);
    });

    //write file data in response body 
    endpoints.MapGet("/downloadstream", async (HttpContext context) =>
    {
        var filePath = "D:\\wjipoccm.3c3";
        await using var fileStream = File.OpenRead(filePath);
        await fileStream.CopyToAsync(context.Response.Body);
    });
    //write file data in response body but did not buffer data by using body writer
    endpoints.MapGet("/downloadstreamwriter", async (HttpContext context) =>
    {
        var filePath = "D:\\wjipoccm.3c3";
        await using var fileStream = File.OpenRead(filePath);
        await fileStream.CopyToAsync(context.Response.BodyWriter);
    });
});

app.Run();
