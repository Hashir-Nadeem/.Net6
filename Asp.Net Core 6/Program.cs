using Asp.Net_Core_6.CustomMiddleware;
using Microsoft.AspNetCore.ResponseCompression;
using System;
using System.IO.Pipelines;
using System.Linq;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<CustomMiddleware2>();

builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.Providers.Add<GzipCompressionProvider>();

});

var app = builder.Build();

app.UseResponseCompression();

/*These are the middlewares that will execute for all the requestes and response and if we use routing before that so all of these will not execute
 and app.userouting() will map that request directly to some endpoint */

//app.UseMiddleware<CustomMiddleware2>();
//app.UseCustomMiddleware1();

//adding custom response headers
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    context.Response.Headers.Add("CustomHeader", "CustomData");
    await next(context);
});

//For complex loggin we can move to custom middlewares for logging
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
    logger.LogInformation(context.Request.Method);
    logger.LogInformation(context.Request.Path);
    await next(context);
});


//conditional middleware with accessing the query parameters
app.UseWhen
    (
     context => context.Request.Path.Value.Contains("/download"),
     app =>
     {
         //URL Rewriting

         app.Use(async (HttpContext context, RequestDelegate next) =>
         {
             if (context.Request.Path.Value.Contains("/download"))
             {
                 context.Request.Path = "/downloadStream";
             }
             await next(context);
         });

         app.Use(async (HttpContext context, RequestDelegate next) =>
         {
             var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
             logger.LogInformation(context.Request.Method);
             logger.LogInformation(context.Request.Path);
             await next(context);
         });
     }
    ); ;

//Short Circuiting Middleware
/*app.Run(async (HttpContext context) =>
{
    await context.Response.WriteAsync("Short Ciruiting Middleware");
});*/


app.UseRouting();

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
        var filePath = "D:\\0rd1iux4.rik";
        await using var fileStream = File.OpenRead(filePath);
        await fileStream.CopyToAsync(context.Response.Body);
    });
    //write file data in response body but did not buffer data by using body writer
    endpoints.MapGet("/downloadstreamwriter", async (HttpContext context) =>
    {
        var filePath = "D:\\0rd1iux4.rik";
        await using var fileStream = File.OpenRead(filePath);
        await fileStream.CopyToAsync(context.Response.BodyWriter);
    });

    //url redirection
    endpoints.Map("/searchengine", (HttpContext context) =>
    {
        context.Response.Redirect("https://www.google.com/");
    });

    endpoints.MapGet("/compresseddata", () =>
    
        "Hello compressed world"
    );
});

app.Run();