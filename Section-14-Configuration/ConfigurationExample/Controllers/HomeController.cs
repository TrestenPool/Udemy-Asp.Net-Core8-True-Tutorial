using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ConfigurationExample.Controllers;

[Route("/")]
public class HomeController : Controller{
  private readonly IConfiguration _configuration;

  // constructor
  public HomeController(IConfiguration configuration){
    // di 
    _configuration = configuration;
  }

  public IActionResult Index(){
    ViewData["result"] = _configuration.GetValue<string>("MyKey","no MyKey supplied in appsettings.json");
    return View();
  }
}