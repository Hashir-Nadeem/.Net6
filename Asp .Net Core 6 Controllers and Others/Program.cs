var builder = WebApplication.CreateBuilder(args);
//addcontrollers vs addcontrollerwithviews vs addmvc
//this will register all the controllers suffix with controller or using controller attribute
builder.Services.AddControllers();
builder.Services.AddTransient<IHttpContextAccessor,HttpContextAccessor>();
var app = builder.Build();

app.Use(async (HttpContext context,RequestDelegate next) =>
{
    await next(context);
});
//this enables us to do caching of response
app.UseResponseCaching();
//if we want to serve files as the response
app.UseStaticFiles();
//we can defined both kind of routing in our application and if for any action and contoller there is no attribute rouitng so this conventional will work
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute
    (
        name:"default",
        pattern:"{controller=Home}/{action=Index}"
    );

    endpoints.MapControllerRoute
    (
        name: "default",
        pattern: "api/v1/{controller=Home}/{action=Index}"
    );

});

//it will enable routing automtically and consider each action method as route
app.MapControllers();


app.Run();
