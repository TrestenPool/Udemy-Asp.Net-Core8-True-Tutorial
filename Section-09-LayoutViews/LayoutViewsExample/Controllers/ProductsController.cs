using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LayoutViewsExample.Controllers;

[Route("/products")]
public class ProductsController : Controller{
  
  [Route("")]
  public IActionResult Index(){
    return View();
  }

  [Route("search/{productId:int:range(0,100000)?}")]
  public IActionResult Search([FromRoute]int? productId){
    ViewData["productId"] = productId;
    return View();
  }

  [Route("order")]
  public IActionResult Order(){
    return View();
  }
}