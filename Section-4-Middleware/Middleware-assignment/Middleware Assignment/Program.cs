using Middleware_Assignment.CustomMiddleware;

namespace Middleware_Assignment {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            // running custom middleware
            app.UseCustomMiddlewareClass();

            app.Use(async(context, next) => {
                await context.Response.WriteAsync("in the other middleware");
                await next();
            });

            app.Run();
        }
    }
}
