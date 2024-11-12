
using System.ComponentModel.Design.Serialization;
using System.Runtime.CompilerServices;

namespace Middleware_intro.CustomMiddleware;

/******** Using interface for middleware class ********/
public class MyCustomMiddleware : IMiddleware {

    // our custom middleware
    public async Task InvokeAsync(HttpContext context, RequestDelegate next) {
        // write to the display
        await context.Response.WriteAsync("--- Custom middleware start ---\n");

        // go to the next chain
        await next(context);

        // write to display at end
        await context.Response.WriteAsync("--- Custom middleware end ---\n");
    }

}

public static class MyCustomMiddlewareExtension {
    public static IApplicationBuilder UseMyCustomMiddleware(this IApplicationBuilder app) {
        return app.UseMiddleware<MyCustomMiddleware>();
    }
}

