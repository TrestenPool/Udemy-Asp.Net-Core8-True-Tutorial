using Microsoft.AspNetCore.Mvc;

namespace HttpRequestExample.Controllers;

[Route("/")]
public class HomeController : Controller {
  private readonly IConfiguration _config;

  public HomeController(IConfiguration configuration) {
    _config = configuration;
  }

  [Route("")]
  public ActionResult Index() {
    return View();
  }



} 