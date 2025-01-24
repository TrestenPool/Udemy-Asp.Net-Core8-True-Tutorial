using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SocialMediaAssignment.Options;

namespace  SocialMediaAssignment.Controllers;

[Route("/")]
[Controller]
public class HomeController : Controller {
  private readonly SocialMediaOptions _options;

  public HomeController(IOptions<SocialMediaOptions> options) {
    _options = options.Value;
  }

  [Route("home")]
  [Route("")]
  public IActionResult Index() {
    ViewData["links"] = _options;
    return View();
  }

}