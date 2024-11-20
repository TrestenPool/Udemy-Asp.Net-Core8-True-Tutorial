namespace RouteParameters {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            // enable routing
            app.UseRouting();

            /*
                no longer have to nest inside of app.UseEndpoints.
                Instead it is recommended to define at the top level like the example below this comment

                app.UseEndpoints(endpoints => {
                    endpoints.MapGet("files/{filename}.{extension}", async (context) => {
                    await context.Response.WriteAsync("In files");
                    });
                });
            */

            #pragma warning disable
            app.UseEndpoints(endpoints => {
                // files
                endpoints.Map("/files/{filename}.{extension}", async (context) => {
                    string filename = context.Request.RouteValues["filename"].ToString();
                    string extension = context.Request.RouteValues["extension"].ToString();
                    await context.Response.WriteAsync($"In files {filename} {extension}\n");
                });

                // employees
                endpoints.Map("employee/profile/{employeename=TrestenPool}", async (context) => {
                    string empName = context.Request.RouteValues["employeename"].ToString();
                    await context.Response.WriteAsync($"In employee profile {empName}\n");
                });

                // products
                endpoints.MapGet("products/details/{id:int?}", async context => {
                    string? id = null;
                    if(context.Request.RouteValues.ContainsKey("id")) {
                        id = context.Request.RouteValues["id"].ToString();
                    }
                    await context.Response.WriteAsync($"In products {id ?? "no id provided"}\n");
                });

            });
            #pragma warning enable

            // defining routes
            //app.MapGet("files/{filename}.{extension}", async (context) => {
            //    await context.Response.WriteAsync("In files");
            //});

            // this middleware will catch all other routes that are not listed above
            app.Run(async (context) => {
                await context.Response.WriteAsync($"Request received at {context.Request.Path}");
            });

            app.Run();
        }
    }
}
