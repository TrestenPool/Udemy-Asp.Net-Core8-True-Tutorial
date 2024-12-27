using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ViewsExample.Controllers;

[Route("/products")]
public class ProductsController: Controller{

  [Route("all")]
  public IActionResult Index() {
    return View();
  }

  [Route("shared")]
  public IActionResult SharedFile() {
    return View("Fake");
  }

}