using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services;
using ServiceContracts;

namespace DIExample.Controllers;

[Route("/")]
public class HomeController : Controller{
  // Variables
  // private readonly ICitiesService citiesService;

  // constructor
  public HomeController() {
    // this.citiesService = citiesService;
  }

  
  [Route("")]
  [Route("home")]
  public IActionResult Index([FromServices] ICitiesService citiesService){
    // get the list of cities
    List<string> cities = citiesService.GetCities();
    return View(cities);
  }
}