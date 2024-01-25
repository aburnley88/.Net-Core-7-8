using System.Runtime.CompilerServices;

namespace UseMethod
{
    public class CustomMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await context.Response.WriteAsync("\ncustom middleware starts\n");
            await next(context);
            await context.Response.WriteAsync("\ncustom middleware ends");
        }
    }

    public static class CustomMiddlewareExtension{
        
        public static IApplicationBuilder UseCustomMiddleware(this IApplicationBuilder app) => 
            app.UseMiddleware<CustomMiddleware>();
        
        
    }

}
