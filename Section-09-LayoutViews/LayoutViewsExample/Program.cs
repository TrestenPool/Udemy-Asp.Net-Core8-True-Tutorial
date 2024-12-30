var builder = WebApplication.CreateBuilder(args);

// add mvc
builder.Services.AddControllersWithViews();

var app = builder.Build();

// add controller support
app.MapControllers();

// add routing
app.UseRouting();

// add static files
app.UseStaticFiles();

app.Run();
