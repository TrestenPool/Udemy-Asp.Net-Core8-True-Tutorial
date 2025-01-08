using ServiceContracts;
using Services;

var builder = WebApplication.CreateBuilder(args);

// add to our ioc container
// builder.Services.Add(new ServiceDescriptor(
//   typeof(ICitiesService),
//   typeof(CitiesListService),
//   ServiceLifetime.Transient
// ));

// per injection
// builder.Services.AddScoped<ICitiesService, CitiesListService>();

// Per scope (browser request)
// builder.Services.AddTransient<ICitiesService, CitiesListService>();

// For Entire application lifetime
builder.Services.AddSingleton<ICitiesService, CitiesListService>();


// add mvc support
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();