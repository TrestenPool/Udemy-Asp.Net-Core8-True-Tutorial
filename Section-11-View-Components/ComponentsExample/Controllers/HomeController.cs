using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ComponentsExample.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ComponentsExample.Controllers;

[Route("/")]
public class HomeController : Controller{

  [Route("")]
  [Route("home")]
  public IActionResult Index(){
    return View();
  }

  [Route("about")]
  public IActionResult About(){
    return View();
  }

  [Route("friends")]
  public IActionResult LoadFriendsList(){

    PersonGridModel personGridModel = new PersonGridModel(){
      GridTitle="FriendsList",
      Persons=new List<Person>(){
        new Person(){Name="Jon",JobTitle="Refinery worker"},
        new Person(){Name="Matt",JobTitle="Physical Therapy"},
        new Person(){Name="Alan",JobTitle="Ranch hand"}
      }
    };

    return ViewComponent("Grid", personGridModel);
  }

}