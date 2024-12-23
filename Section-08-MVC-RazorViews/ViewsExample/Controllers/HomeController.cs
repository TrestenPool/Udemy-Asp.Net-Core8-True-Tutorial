using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ViewsExample.Controllers;

[Route("/")]
public class HomeController : Controller{

  // index page
  [Route("")]
  [Route("home")]
  public IActionResult Index() {
    return View(); // Views/Home/Index.cshtml
  }

}