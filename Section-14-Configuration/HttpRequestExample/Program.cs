using HttpRequestExample.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

// Registers IHttpClientFactory
builder.Services.AddHttpClient();
builder.Services.AddScoped<MyService>();


// Add http client
builder.Services.AddHttpClient();

// load custom config
builder.Configuration.AddJsonFile("customConfig.json", optional: true);

var app = builder.Build();

app.MapControllers();
app.UseRouting();
app.UseStaticFiles();


app.Run();
