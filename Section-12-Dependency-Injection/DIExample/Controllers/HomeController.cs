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
  // // Variables
  private readonly CitiesListService citiesListService;

  // // constructor
  public HomeController() {
    citiesListService = new CitiesListService();
    citiesListService.Name = "Test name";
  }

  
  [Route("")]
  [Route("home")]
  public IActionResult Index(){
    return View();
  }
}