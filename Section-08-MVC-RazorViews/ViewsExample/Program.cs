var builder = WebApplication.CreateBuilder(args);

// add mvc
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseStaticFiles();
app.MapControllers();

app.Run();
