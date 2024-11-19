using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.OutputCaching;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;

namespace Middleware_Assignment.CustomMiddleware {
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class CustomMiddlewareClass {
        private readonly RequestDelegate _next;

        public CustomMiddlewareClass(RequestDelegate next) {
            _next = next;
        }

        public async Task Invoke(HttpContext context) {
            // get the path of the request
            string path = context.Request.Path;
            string method = context.Request.Method;

            if(method != "POST") {
                await context.Response.WriteAsync("Must use POST request for this route!\n");
                await _next(context);
                return;
            }

            // read in the body of the request
            var reader = new StreamReader(context.Request.Body);
            string body = await reader.ReadToEndAsync();

            // place the body of the request in a query string to parse
            Dictionary<string,StringValues> queryString = QueryHelpers.ParseQuery(body);

            if(queryString.ContainsKey("Username") && queryString.ContainsKey("Password")) {
                var username = queryString["username"];
                var password = queryString["password"];
                if(username == "admin@example.com" && password == "admin1234") {
                    await context.Response.WriteAsync("Success!!\n");
                }
                else {
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsync("invalid login...\n");
                }
            }
            else {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Invalid input for 'email'\nInvalid input for 'password'\n");
            }

            await _next(context);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class CustomMiddlewareClassExtensions {
        public static IApplicationBuilder UseCustomMiddlewareClass(this IApplicationBuilder builder) {
            return builder.UseMiddleware<CustomMiddlewareClass>();
        }
    }
}
