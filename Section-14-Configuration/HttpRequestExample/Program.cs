var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

// load custom config
builder.Configuration.AddJsonFile("customConfig.json", optional: true);

var app = builder.Build();

app.MapControllers();
app.UseRouting();
app.UseStaticFiles();


app.Run();
