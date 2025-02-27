using Microsoft.AspNetCore.Mvc;

[Route("/")]
public class PersonsController: Controller {

  [Route("persons/index")]
  public IActionResult Index() {
    return View();
  }

}