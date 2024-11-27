using Sample1.Controllers;

namespace Sample1;

public class Program
{
    public static void Main(string[] args)
    {
        /***************** Get builder *******************/
        var builder = WebApplication.CreateBuilder(args);

        // add multiple controllers without having to specify each one
        builder.Services.AddControllers();

        /***************** Build or app *******************/
        var app = builder.Build();

       // map controllers in 1 step
       app.MapControllers();

        // app.Run(async context => {
        //   await context.Response.WriteAsync("hi mate");
        // });

        app.Run();
    }
}
