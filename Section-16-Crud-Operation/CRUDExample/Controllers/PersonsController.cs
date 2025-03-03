using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using Services;

[Route("/")]
public class PersonsController: Controller {
  // private fields
  private readonly IPersonService _personService;

  // constructor
  public PersonsController(IPersonService personService) {
    _personService = personService;
  }

  [Route("persons/index")]
  public IActionResult Index() {
    ViewData["persons"] = _personService.GetAllPersons();
    ViewData["SearchFields"] = new Dictionary<string,string>(){
      {nameof(PersonResponse.PersonName), "Name"},
      {nameof(PersonResponse.Email), "Email"},
      {nameof(PersonResponse.DateOfBirth), "Date of birth"},
      {nameof(PersonResponse.PersonGender), "Gender"},
      {nameof(PersonResponse.Age), "Age"},
    };
    return View();
  }

}