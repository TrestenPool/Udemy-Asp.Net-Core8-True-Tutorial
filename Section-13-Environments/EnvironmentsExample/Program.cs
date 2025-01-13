var builder = WebApplication.CreateBuilder(args);
// add mvc support
builder.Services.AddControllersWithViews();
var app = builder.Build();

if(app.Environment.IsDevelopment()){
  Console.WriteLine("======== DEVELOPMENT MODE =======");
  app.UseDeveloperExceptionPage();
}
else {
  Console.WriteLine("======== NOT DEVELOPMENT MODE =======");
}

app.MapControllers();
app.UseStaticFiles();
app.UseRouting();

app.Run();
