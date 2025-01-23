using HttpRequestExample.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Registers IHttpClientFactory
builder.Services.AddHttpClient();
builder.Services.AddScoped<FinnhubService>();

// Add http client factory IHttpClientFactory to be injected for use in services
builder.Services.AddHttpClient();

// load custom config
builder.Configuration.AddJsonFile("customConfig.json", optional: true);

var app = builder.Build();

// must provide api key
if( app.Configuration["ApiKey"] == null ) {
  throw new Exception("You must provide a key 'ApiKey' in the secrets");
}

app.MapControllers();
app.UseRouting();
app.UseStaticFiles();


app.Run();
