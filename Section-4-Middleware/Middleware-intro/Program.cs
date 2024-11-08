namespace Middleware_intro {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();


            // middlware 1
            app.Use(async (HttpContext context, RequestDelegate next) => {
                await context.Response.WriteAsync("Hello mate, I am the first middleware");
                await next(context);
            });

            // middleware 2
            app.Use(async (HttpContext context, RequestDelegate next) => {
                await context.Response.WriteAsync("\nBye, I am the second middleware");
                await next(context);
            });


            // middleware 3 (aka terminating middleware)
            app.Run(async (HttpContext context) => {
                await context.Response.WriteAsync("\n\nAnd I'm at the end");
            });

            app.Run();
        }
    }
}
