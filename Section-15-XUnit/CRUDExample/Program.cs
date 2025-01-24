var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();

var app = builder.Build();
app.UseStaticFiles();
app.UseRouting();

// enable exception page if in development mode
if(builder.Environment.IsDevelopment()) {
  app.UseDeveloperExceptionPage();
}

app.MapControllers();

app.Run();