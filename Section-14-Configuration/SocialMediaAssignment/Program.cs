using SocialMediaAssignment.Options;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();

// Options pattern
builder.Services.Configure<SocialMediaOptions>(
  builder.Configuration.GetSection("SocialMediaLinks")
);

var app = builder.Build();

app.UseStaticFiles();

app.MapControllers();

app.Run();
