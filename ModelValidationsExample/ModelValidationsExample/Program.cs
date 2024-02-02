using ModelValidationsExample.CustomModelBinding;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers(options =>
{
   // options.ModelBinderProviders.Insert(0, new PersonModelBinderProvider());
});
builder.Services.AddControllers().AddXmlDataContractSerializerFormatters();
var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();
app.MapGet("/", () => "Hello World!");

app.Run();
