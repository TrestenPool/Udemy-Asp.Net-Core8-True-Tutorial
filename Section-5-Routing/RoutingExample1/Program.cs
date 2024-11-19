namespace RoutingExample1 {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            // show that GetEndpoint() returns null
            app.Use(async (context, next) => {
                Console.WriteLine($"before: endpoint = {context.GetEndpoint()?.DisplayName}");
                await next(context);
            });

            /*
                Enables routing and selects an appropriate end point based on the url path and http method.
                this by itself doesn't really do anything. You have to use with app.UseEndpoints
            */
            app.UseRouting();

            // show that GetEndpoint() now returns something because it is after UseRouting()
            app.Use(async (context, next) => {
                Console.WriteLine($"after: endpoint = {context.GetEndpoint()?.DisplayName}");
                await next(context);
            });




            #pragma warning disable

            // create the endpoints
            app.UseEndpoints(endpoints => {

                // home path, only get request
                endpoints.MapGet("/", async (HttpContext context) => {
                    await context.Response.WriteAsync("HOME path");
                });

                // /something path, map any method to this endpoint
                endpoints.Map("/something", async (context) => {
                    await context.Response.WriteAsync("something path");
                });

                // /hi path, only get requests
                endpoints.MapGet("/hi", async (HttpContext context) => {
                    await context.Response.WriteAsync("Hi world");
                });

            });

            #pragma warning enable

            app.Run();
        }
    }
}
