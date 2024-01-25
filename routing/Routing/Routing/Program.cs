using Routing;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRouting(opts =>
{
    opts.ConstraintMap.Add("months", typeof(MonthConstraint));
});

var app = builder.Build();

app.UseRouting();

#pragma warning disable ASP0014 // Suggest using top level route registrations
app.UseEndpoints(endpoints =>
{
    endpoints.Map("files/{filename}.{ext}", async (context) =>
    {
        string? fileName = Convert.ToString(context.Request.RouteValues["filename"]);
        string? fileExt = Convert.ToString(context.Request.RouteValues["ext"]);
        var fullName = fileName + '.'+ fileExt;
        await context.Response.WriteAsync($"<h1>{fullName} in files </h1>");
    });

    endpoints.Map("employee/profile/{employeename:length(3,10):alpha=Arnold}", async context =>
    {
        string? name = Convert.ToString(context.Request.RouteValues["employeename"]);
        await context.Response.WriteAsync($"Name: {name}");
    });

    endpoints.Map("/product/details/{id:range(1,99):int?}", async ctx =>
    {
        string response = ctx.Request.RouteValues.ContainsKey("id") ? 
        $"Product Details - {Convert.ToInt32(ctx.Request.RouteValues["id"])}" : "Product Id not suplied";
        await ctx.Response.WriteAsync(response); 
    });

    //daily report
    endpoints.Map("daily-report/{reportdate:datetime}", async ctx =>
    {
        DateTime reportDate = Convert.ToDateTime(
            ctx.Request.RouteValues["reoirtdate"]);
        await ctx.Response.WriteAsync($"In daily report: {reportDate.ToShortDateString()}");
    });

    //eg cities/cityid
    endpoints.Map("cities/{cityid:guid}", async ctx =>
    {
        Guid cityid = Guid.Parse(Convert.ToString(ctx.Request.RouteValues["cityid"])!);
        await ctx.Response.WriteAsync($"City Information- {cityid}");
    });

    //report example for regex
    endpoints.Map("sales-report/{year:int:min(1990)}/{month:months}", async ctx =>
    {
        int year = Convert.ToInt32(ctx.Request.RouteValues["year"]);
        string? month = Convert.ToString(ctx.Request.RouteValues["month"]);

        await ctx.Response.WriteAsync($"sales report - {year} - {month}");
    });
    //specific sales report to demonstrate precedence
    endpoints.Map("sales-report/2024/jan", async ctx =>
    {
        await ctx.Response.WriteAsync("Running in the middleware made for me!");
    });
});

app.Run(async ctx =>
{
   await ctx.Response.WriteAsync($"No route match at '{Convert.ToString(ctx.Request.Path)}'");
});
#pragma warning restore ASP0014 // Suggest using top level route registrations

app.Run();
