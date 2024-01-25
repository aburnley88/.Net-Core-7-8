using System.Text;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseRouting();

#pragma warning disable ASP0014 // Suggest using top level route registrations
app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/countries", async ctx =>
    {
        StringBuilder sb = new();
        var countries = app.LoadCountries();
        foreach (var country in countries)
        {
            sb.Append(Convert.ToString(country.Key) + ". " + country.Value + "\n");
        }
        await ctx.Response.WriteAsync(sb.ToString().Trim());
    });

    endpoints.MapGet("countries/{countryid:int:range(1, 100)}", async ctx =>
    {
        var countries = app.LoadCountries();
        int countryId = Convert.ToInt32(ctx.Request.RouteValues["countryid"]);
        string response = countryId < 5 ? countries[countryId] : "No Country";

        if(countryId > 5)
        {
            ctx.Response.StatusCode = 404;
            await ctx.Response.WriteAsync(response);
        }
        else
        {
            await ctx.Response.WriteAsync(response);
        }
        
    });

    endpoints.MapGet("countries/{countryid:int:min(101)=101}", async ctx =>
    {
        ctx.Response.StatusCode = 400;
        await ctx.Response.WriteAsync("The CountryId should be between 1 and 100");
    });
});
#pragma warning restore ASP0014 // Suggest using top level route registrations

app.Run(async (ctx) => {
    await ctx.Response.WriteAsync("Hello World");
});

app.Run();


public static class MyExtensions
{
    public static Dictionary<int, string> LoadCountries(this IApplicationBuilder app)
    {
        return new Dictionary<int, string>()
        {

            {1, "United States"},
            {2, "Canada" },
            {3, "United Kingdom" },
            {4, "India" },
            {5, "Japan" }
        };
    }
                       
}