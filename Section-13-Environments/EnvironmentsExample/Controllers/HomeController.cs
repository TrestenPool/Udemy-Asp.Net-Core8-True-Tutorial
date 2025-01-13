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
  [Route("")]
  public IActionResult Index(){
    return View();
  }

  // trigger error for development error page
  // [Route("")]
  // public IActionResult Other(){
  //   return View();
  // }

}