using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services;

namespace DIExample.Controllers;

[Route("/")]
public class HomeController : Controller{
  // Variables
  private readonly CitiesListService citiesListService;

  // constructor
  public HomeController() {
    // you should never do this. why??
    citiesListService = new CitiesListService();
  }

  
  [Route("")]
  [Route("home")]
  public IActionResult Index(){
    // get the list of cities
    List<string> cities = citiesListService.GetCitiesList();
    return View(cities);
  }
}