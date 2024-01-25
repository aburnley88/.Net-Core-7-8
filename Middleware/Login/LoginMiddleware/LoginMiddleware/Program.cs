using LoginMiddleware;
using System.ComponentModel.DataAnnotations;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


app.UseLoginMiddleware();

app.Run();



