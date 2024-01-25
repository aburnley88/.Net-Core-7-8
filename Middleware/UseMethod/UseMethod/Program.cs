using UseMethod;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<CustomMiddleware>();
var app = builder.Build();

//Middleware 1
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("Hello from Middleware 1. \n I call the next middleware with the delegate.\n");
    await next(context); 
});

//custom (middleware 2)
//app.UseCustomMiddleware();
app.UseConventionalMiddleware();

//middleware 3
app.Run(async (context) =>
{
    await context.Response.WriteAsync("Hello from 3rd Middleware");
});

app.Run();
