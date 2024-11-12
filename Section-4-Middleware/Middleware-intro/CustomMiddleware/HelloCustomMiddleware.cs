using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Middleware_intro.CustomMiddleware; 

public class HelloCustomMiddleware {
    private readonly RequestDelegate _next;

    // asp.net will use depend Inj to pass in the params
    public HelloCustomMiddleware(RequestDelegate next) {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext) {

        // check if query string contains both keys
        if( httpContext.Request.Query.ContainsKey("firstName") 
            && httpContext.Request.Query.ContainsKey("lastName" ) ) {

            // make a fullname string
            string fullName = httpContext.Request.Query["firstName"] + " " +
                httpContext.Request.Query["lastName"];

            // show the user
            await httpContext.Response.WriteAsync($"fullname = {fullName}\n");
        }

        await _next(httpContext);
        // after logic
    }
}


// Extension method used to add the middleware to the HTTP request pipeline.
public static class HelloCustomMiddlewareExtensions {
    public static IApplicationBuilder UseHelloCustomMiddleware(this IApplicationBuilder builder) {
        return builder.UseMiddleware<HelloCustomMiddleware>();
    }
}
