using Microsoft.AspNetCore.Mvc;

namespace CRUDExample.Controllers;

[Route("/")]
public class HomeController : Controller {
  [Route("")]
  public IActionResult Index() {
    return View();
  }
}