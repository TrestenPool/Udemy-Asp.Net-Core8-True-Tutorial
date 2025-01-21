using ConfigurationExample;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

// register the options pattern class
builder.Services.Configure<UserdataOptions>(
  builder.Configuration.GetSection("userdata")
);

// load custom config json file
builder.Configuration.AddJsonFile("myOwnConfig.json", optional: true, reloadOnChange: true);

var app = builder.Build();

app.MapControllers();
app.UseStaticFiles();
app.UseRouting();



// app.UseEndpoints(endpoints => {

//   var _ = endpoints.Map("/", async context => {
//     // get the value from appsettings.json
//     string keyvalue = app.Configuration["MyKey"] ?? "no MyKey key present";
//     var othervalue = app.Configuration.GetValue<string>("othervalue","no key called othervalue");

//     // return the response to the page
//     await context.Response.WriteAsync(keyvalue + "\n");
//     await context.Response.WriteAsync(othervalue);
//   });

// });

app.Run();
