using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ServiceContracts;
using Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();

// registering an interface with a concrete type
builder.Services.AddScoped<ICountriesService, CountriesService>();
builder.Services.AddScoped<IPersonService, PersonService>();

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

// enable exception page if in development mode
if(builder.Environment.IsDevelopment()) {
  app.UseDeveloperExceptionPage();
}

app.MapControllers();

app.Run();