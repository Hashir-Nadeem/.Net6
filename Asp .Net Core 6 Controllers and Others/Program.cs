using Asp_.Net_Core_6_Controllers_and_Others.CustomConstraints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

//wwwroot folder must be exist before having another global static file folder
var builder = WebApplication.CreateBuilder
    (
        new WebApplicationOptions()
        {
            WebRootPath = "staticFiles"
        }
    ); 
//addcontrollers vs addcontrollerwithviews vs addmvc
//this will register all the controllers suffix with controller or using controller attribute
builder.Services.AddControllers();
builder.Services.AddTransient<IHttpContextAccessor,HttpContextAccessor>();

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
});

//register custom constraint
builder.Services.AddRouting(options =>
{
    options.ConstraintMap.Add("isHashir", typeof(CustomConstraint1));
});
var app = builder.Build();

app.Use(async (HttpContext context,RequestDelegate next) =>
{
    await next(context);
});
//this enables us to do caching of response
app.UseResponseCaching();
//if we want to serve files as the response
app.UseStaticFiles();

//incase of more than one folder to serve
app.UseStaticFiles(new StaticFileOptions() { FileProvider=new PhysicalFileProvider(builder.Environment.ContentRootPath+"\\staticFilesFolder")});
//we can defined both kind of routing in our application and if for any action and contoller there is no attribute rouitng so this conventional will work
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    //default paratmeters and optional parameters
    endpoints.MapControllerRoute
    (
        name:"default",
        pattern:"{controller=Home}/{action=Index}/{filename?}"
    );

    endpoints.MapControllerRoute
    (
        name: "catch-all",
        pattern: "Home/**",
        defaults: new { controller = "Home", action = "Index" }

    );

});
//it will enable routing automtically and consider each action method as route
app.MapControllers();


app.Run();
