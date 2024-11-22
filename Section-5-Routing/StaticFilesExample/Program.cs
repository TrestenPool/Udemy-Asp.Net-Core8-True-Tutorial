namespace StaticFilesExample {
    public class Program {
        public static void Main(string[] args) {

            // create the builder
            var builder = WebApplication.CreateBuilder(
                new WebApplicationOptions() {
                    WebRootPath = "myroot"
                }
             );

            var app = builder.Build();

            app.UseStaticFiles();
            app.UseRouting();


            app.UseEndpoints(endpoints => {
                endpoints.MapGet("/", async context => {
                    await context.Response.SendFileAsync("myroot/dog.txt");
                });
            });

            app.Run();
        }
    }
}
