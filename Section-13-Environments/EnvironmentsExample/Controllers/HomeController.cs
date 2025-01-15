using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EnvironmentsExample.Controllers;

[Route("/")]
public class HomeController : Controller{

  private readonly IWebHostEnvironment _webHostEnvironment;

  public HomeController(IWebHostEnvironment webHostEnvironment){
    _webHostEnvironment = webHostEnvironment;
  }
  
  [Route("")]
  public IActionResult Index(){
    // set viewdata key with environment name
    ViewData["environment"] = _webHostEnvironment.EnvironmentName;
    return View();
  }

  // trigger error for development error page
  // [Route("")]
  // public IActionResult Other(){
  //   return View();
  // }

}