using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PartialViewsExample.Models;

namespace PartialViewsExample.Controllers;

[Route("/")]
public class HomeController : Controller{
    
  [Route("")]
  public IActionResult Index(){
    return View();
  }

  [Route("about")]
  public IActionResult About(){
    return View();
  }

  [Route("programming")]
  public IActionResult ProgrammingLanguages() {

    // our listmodel object
    ListModel listModel = new ListModel();
    listModel.ListTitle = "Programming Languages";
    listModel.ListItems = new List<string>(){
      "C",
      "Python",
      "Java",
      "SQL",
      "Powershell"
    };

    return PartialView("_ListPartialView",listModel);
  }

}