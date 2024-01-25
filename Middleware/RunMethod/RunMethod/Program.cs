var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run(async (context) =>
{
    await context.Response.WriteAsync("Hello, I shortcuirt the middleware pipeline");
});

app.Run(async (context) =>
{
    await context.Response.WriteAsync("Hello, I wont render because the previous Run method is terminal by default");
});
app.Run();
