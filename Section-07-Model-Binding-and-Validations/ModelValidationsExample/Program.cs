using ModelValidationsExample.CustomModelBinders;

namespace ModelValidationsExample;

public class Program
{
    public static void Main(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args);
      builder.Services.AddControllers(options => {
        // options.ModelBinderProviders.Insert(0, new PersonBinderProvider());
      });
      // how to add xml support for reading body of request
      // builder.Services.AddControllers().AddXmlSerializerFormatters();
      var app = builder.Build();
      app.MapControllers();
      app.UseStaticFiles();

      app.Run();
    }
}
