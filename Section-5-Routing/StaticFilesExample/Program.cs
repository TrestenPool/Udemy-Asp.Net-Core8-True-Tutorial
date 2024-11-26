using Microsoft.Extensions.FileProviders;

namespace StaticFilesExample {
    public class Program {
        public static void Main(string[] args) {

            // create the builder
            var builder = WebApplication.CreateBuilder(
                // define the name of the web root path
                new WebApplicationOptions() {
                    WebRootPath = "myroot"
                }
             );

            var app = builder.Build();

            // setup the 1st webroot folder
            app.UseStaticFiles();

            // setup the 2nd webroot folder
            app.UseStaticFiles(new StaticFileOptions() {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(builder.Environment.ContentRootPath, "mywebroot"))
            });
            // ...

            app.UseRouting();


            // define our endpoints
            app.UseEndpoints(endpoints => {
                // map an endpoint to get the dog file
                endpoints.MapGet("dog", async context => {
                    await context.Response.SendFileAsync("myroot/dog.txt");
                });
                endpoints.MapGet("cat", async context => {
                    await context.Response.SendFileAsync("mywebroot/cat.txt");
                });
            });

            // catch all
            app.Run(async (context) => {
                await context.Response.WriteAsync($"Request made to path {context.Request.Path}");
            });

            app.Run();
        }
    }
}
