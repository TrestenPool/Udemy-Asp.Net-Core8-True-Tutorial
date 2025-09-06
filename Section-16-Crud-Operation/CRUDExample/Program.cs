using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using OfficeOpenXml;
using ServiceContracts;
using Services;

var builder = WebApplication.CreateBuilder(args);

// add the controllers
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();

// registering an interface with a concrete type
builder.Services.AddScoped<ICountriesService, CountriesService>();
builder.Services.AddScoped<IPersonService, PersonService>();

// set the license context for the excel nuget package
// ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

// register the persondbcontext
builder.Services.AddDbContext<PersonsDbContext>(
  options => {
    options

    // only get these log messages
    // .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Name })

    // enables lazy loading
    // .UseLazyLoadingProxies()

    // connection to the db
    .UseSqlServer(builder.Configuration.GetConnectionString("Default"));
  } 
);

Console.WriteLine("===========================================");
Console.WriteLine($"Running in {builder.Environment.EnvironmentName}");
Console.WriteLine("===========================================");

var app = builder.Build();
app.UseStaticFiles();
app.UseRouting();

// setup the path to the html to pdf executable
Rotativa.AspNetCore.RotativaConfiguration.Setup("wwwroot", "Rotativa");

// enable exception page if in development mode
if(builder.Environment.IsDevelopment()) {
  app.UseDeveloperExceptionPage();
}

app.MapControllers();

app.Run();