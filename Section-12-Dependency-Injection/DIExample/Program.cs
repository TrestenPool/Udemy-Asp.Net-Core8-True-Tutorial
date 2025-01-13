using System.ComponentModel;
using Autofac;
using Autofac.Core.Lifetime;
using Autofac.Extensions.DependencyInjection;
using ServiceContracts;
using Services;

var builder = WebApplication.CreateBuilder(args);

/************** Setting up DI Using Autofac **********************/
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// equivalent to AddTransient
builder.Host.ConfigureContainer<ContainerBuilder>(ContainerBuilder => {
  // TRANSIENT
  ContainerBuilder.RegisterType<CitiesListService>().As<ICitiesService>().InstancePerDependency();
  
  // SCOPED
  // ContainerBuilder.RegisterType<ICitiesService>().As<CitiesListService>().InstancePerLifetimeScope();

  // SINGLETON
  // ContainerBuilder.RegisterType<ICitiesService>().As<CitiesListService>().SingleInstance();
});
/*****************************************************************/


// add to our ioc container
// builder.Services.Add(new ServiceDescriptor(
//   typeof(ICitiesService),
//   typeof(CitiesListService),
//   ServiceLifetime.Transient
// ));

// per scope (per request)
// builder.Services.AddScoped<ICitiesService, CitiesListService>();

// Transient (per injection)
// builder.Services.AddTransient<ICitiesService, CitiesListService>();

// For Entire application lifetime
// builder.Services.AddSingleton<ICitiesService, CitiesListService>();


// add mvc support
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();