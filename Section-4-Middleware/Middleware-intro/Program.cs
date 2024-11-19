using Microsoft.Extensions.Diagnostics.HealthChecks;
using Middleware_intro.CustomMiddleware;

namespace Middleware_intro;
public class Program {
    public static void Main(string[] args) {
        // setup the builder
        var builder = WebApplication.CreateBuilder(args);

        // registering the custom middleware
        //builder.Services.AddTransient<MyCustomMiddleware>();
        //builder.Services.AddTransient<CorsBeer>();
        //builder.Services.AddTransient<HelloCustomMiddleware>();

        // get our app
        var app = builder.Build();

        // call our middleware
        /** 
            Using either of these work for some reason
         **/
        //app.UseMiddleware<HelloCustomMiddleware>(); // passing the UseMiddleware function our custom generic class as generic
        app.UseHelloCustomMiddleware(); // calling our extension method, PREFERRED

        // more middleware
        app.Use(async (HttpContext context, RequestDelegate next) => {
            await context.Response.WriteAsync("Hello world\n");
            await next(context);
        });
        app.Use(async (HttpContext context, RequestDelegate next) => {
            await context.Response.WriteAsync("Goodbye world\n");
        });

        app.Run();
    }

}
