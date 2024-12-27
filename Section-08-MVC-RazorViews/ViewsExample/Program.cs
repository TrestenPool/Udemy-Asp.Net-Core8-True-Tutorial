var builder = WebApplication.CreateBuilder(args);

// register the necessary services for MVC with the dependency injection
/*
  Shortcut for adding mulitple services at once
  - Automatically discover controller classes in your project
  - sets Razor as the view engine
  - Model binding, sets up model binding for handling submittions
  - Enables model validation
*/
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseStaticFiles();
app.MapControllers();

app.Run();
