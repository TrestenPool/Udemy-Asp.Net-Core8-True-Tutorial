using Sample1.Controllers;

namespace Sample1;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // add the controller 1 by 1
        // builder.Services.AddTransient<HomeController>();

        // add multiple controllers without having to specify each one
        builder.Services.AddControllers();

        var app = builder.Build();


       // map controllers in 1 step, no need to add the code below this
       app.MapControllers();

        app.UseRouting();

        // app.UseEndpoints(endpoints => {
        //   _ = endpoints.MapControllers();
        // });

        app.Run();
    }
}
