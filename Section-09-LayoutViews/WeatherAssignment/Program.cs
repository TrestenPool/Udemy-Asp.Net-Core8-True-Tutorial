var builder = WebApplication.CreateBuilder(args);
// add support for mvc
builder.Services.AddControllersWithViews();
var app = builder.Build();

// add additional functionality
app.UseRouting();
app.MapControllers();
app.UseStaticFiles();

app.Run();
