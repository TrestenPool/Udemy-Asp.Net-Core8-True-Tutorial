using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ConfigurationExample.Controllers;

/***********
  Example that shows how to use Options using services
************/

[Route("/")]
public class HomeController : Controller{
  private readonly UserdataOptions _options;

  public HomeController(IOptions<UserdataOptions> options){
    _options = options.Value;
  }

  public IActionResult Index(){
    ViewData["username"] = _options.Username;
    ViewData["password"] = _options.Password;

    return View();
  }
}