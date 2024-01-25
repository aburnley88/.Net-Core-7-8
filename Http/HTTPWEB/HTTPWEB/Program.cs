using Microsoft.Extensions.Primitives;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//app.Run(async (HttpContext context) =>
//{
//    string path = context.Request.Path;
//    string reqMethod = context.Request.Method;

//    //string? id = reqMethod == "GET" && context.Request.Query.ContainsKey("id") ? context.Request.Query["id"].ToString() : null;

//    //context.Response.Headers["MyHeader"] = "My Vaue";
//    //context.Response.Headers["Content-Type"] = "text/html";

//    await context.Response.WriteAsync($"<h5>Path: {path}</h5>");
//    await context.Response.WriteAsync($"<h5>Method: {reqMethod}</h5>");
//    //await context.Response.WriteAsync($"<h5>ID: {id}</h5>");
//    context.Response.Headers["Content-Type"] = "text/html";
//    string? userAgent = context.Request.Headers.ContainsKey("User-Agent") ?
//                                context.Request.Headers["User-Agent"].ToString() : null ?? "no value";
//    await context.Response.WriteAsync($"<p>{userAgent}</p>");
//});

app.Run(async (HttpContext context) =>
{
    StreamReader sr = new StreamReader(context.Request.Body);
    string body = await sr.ReadToEndAsync() ?? "no value";

    Dictionary<string, StringValues> dict = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(body);

    string? firstName = dict.ContainsKey("FirstName") ? dict["firstName"][0]?.ToString() : "No Name Provided";

    if (!string.IsNullOrEmpty(firstName))
    {
        await context.Response.WriteAsync($"Name {firstName}");
    }
    
});

app.Run();
