using Entities;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();

// registering an interface with a concrete type
builder.Services.AddScoped<ICountriesService, CountriesService>();
builder.Services.AddScoped<IPersonService, PersonService>();

// add the connection to the sql db
builder.Services.AddDbContext<PersonsDbContext>(
  options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
  } 
);

Console.WriteLine("===========================================");
Console.WriteLine($"Running in {builder.Environment.EnvironmentName}");
Console.WriteLine("===========================================");

var app = builder.Build();
app.UseStaticFiles();
app.UseRouting();

// enable exception page if in development mode
if(builder.Environment.IsDevelopment()) {
  app.UseDeveloperExceptionPage();
}

app.MapControllers();

app.Run();