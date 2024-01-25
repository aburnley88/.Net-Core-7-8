using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace LoginMiddleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class LoginMiddleware
    {
        private readonly RequestDelegate _next;

        public LoginMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            bool isHomePath = context.Request.Path == "/"; 
            bool containsEmail = context.Request.Query.ContainsKey("email");
            bool containsPassword = context.Request.Query.ContainsKey("password");
            bool isPost = context.Request.Method == "POST";
            bool isGet = context.Request.Method == "GET";
            bool validCredentials = context.Request.Query["email"] == Validation._email && context.Request.Query["Password"] == Validation._password;

            int statusCode = isHomePath && validCredentials && isPost ? 200 :
                             isHomePath && isGet ? 200 :
                             isHomePath && isPost && !validCredentials ? 400 :
                             isHomePath && !containsEmail || !containsPassword ? 400 : 400;


            string response = statusCode == 200 && isPost ? "Successful login" :
                              statusCode == 200 && isGet ? "No response" :
                              isHomePath && !validCredentials ? "Invalid login" :
                              isHomePath && !containsEmail && !containsPassword ? "Invalid input for 'email' \n Invalid input for 'password'" :
                              isHomePath && !containsPassword ? "Invalid input for 'password'" : "Invalid input for 'email'"; 
            
            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsync(response);

            await _next(context);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class LoginMiddlewareExtensions
    {
        public static IApplicationBuilder UseLoginMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LoginMiddleware>();
        }
    }

    public static class Validation
    {
        public static readonly string _email = "admin@example.com";
        public static readonly string _password = "admin1234";
    }
}
