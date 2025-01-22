using HttpRequestExample.Services;
using Microsoft.AspNetCore.Mvc;

namespace HttpRequestExample.Controllers;

[Route("/")]
public class HomeController : Controller {
  private readonly IConfiguration _config;
  private readonly MyService _myservice;

  public HomeController(IConfiguration config, MyService myservice) { 
    _config = config;
    _myservice = myservice;
  }

  [Route("")]
  public async Task<ActionResult> Index() {
    await _myservice.Method();
    return View();
  }



} 