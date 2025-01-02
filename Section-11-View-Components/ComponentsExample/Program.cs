var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseStaticFiles();
// enable attribute routing
app.MapControllers();
app.UseRouting();

app.Run();
