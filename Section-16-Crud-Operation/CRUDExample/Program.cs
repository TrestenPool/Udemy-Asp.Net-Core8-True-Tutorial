using ServiceContracts;
using Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();

// registering an interface with a concrete type
builder.Services.AddSingleton<ICountriesService, CountriesService>();
builder.Services.AddSingleton<IPersonService, PersonService>();

var app = builder.Build();
app.UseStaticFiles();
app.UseRouting();

// enable exception page if in development mode
if(builder.Environment.IsDevelopment()) {
  app.UseDeveloperExceptionPage();
}

app.MapControllers();

app.Run();