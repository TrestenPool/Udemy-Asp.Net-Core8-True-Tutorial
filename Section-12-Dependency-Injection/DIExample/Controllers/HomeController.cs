using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services;
using ServiceContracts;
using Autofac;

namespace DIExample.Controllers;

[Route("/")]
public class HomeController : Controller{
  // variables used for di
  private readonly ICitiesService citiesService1;
  private readonly ICitiesService citiesService2;
  private readonly ICitiesService citiesService3;
  
  // used for .net DI implementation
  // private readonly IServiceScopeFactory _serviceScopeFactory;

  // used for autofac
  private readonly ILifetimeScope _lifetimeScope;

  // constructor DI
  public HomeController(
    ICitiesService citiesService1,
    ICitiesService citiesService2,
    ICitiesService citiesService3,
    // autofac impl
    ILifetimeScope lifetimeScope) {
    // .net core impl
    // IServiceScopeFactory serviceScopeFactory) {

    this.citiesService1 = citiesService1;
    this.citiesService2 = citiesService2;
    this.citiesService3 = citiesService3;

    // using autofac
    this._lifetimeScope = lifetimeScope;

    // this._serviceScopeFactory = serviceScopeFactory;
  }

  [Route("")]
  [Route("home")]
  public IActionResult Index(){
    List<string> cities = citiesService1.GetCities();
    ViewData["Guid1"] = citiesService1.ServiceInstanceId;
    ViewData["Guid2"] = citiesService2.ServiceInstanceId;
    ViewData["Guid3"] = citiesService3.ServiceInstanceId;

    // enters child scope, new scope
    // using(var scope = _serviceScopeFactory.CreateScope()){

    //   var citiesService4 = scope.ServiceProvider.GetService<ICitiesService>();
    //   ViewData["Guid4"] = citiesService4?.ServiceInstanceId;

    // } // end of scope; it calls CitiesService.Dispose()

    using(var scope = _lifetimeScope.BeginLifetimeScope()) {
      scope.
    }

    return View(cities);
  }
}