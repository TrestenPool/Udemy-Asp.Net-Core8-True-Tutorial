using System.Runtime.InteropServices.Marshalling;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceContracts;
using ServiceContracts.Enums;
using Services;

[Route("[controller]")]
public class PersonsController: Controller {
  // private fields
  private readonly IPersonService _personService;

  // constructor
  public PersonsController(IPersonService personService) {
    _personService = personService;
  }

  [Route("[action]")]
  public IActionResult Index(
    [FromQuery]string searchBy, 
    [FromQuery]string searchString,
    [FromQuery]string sortBy="PersonName",
    [FromQuery]SortOrderEnum sortOrder=SortOrderEnum.Ascending
    ) {
    
    // populate options to search by
    ViewData["SearchFields"] = new Dictionary<string,string>(){
      {nameof(PersonResponse.PersonName), "Name"},
      {nameof(PersonResponse.Email), "Email"},
      {nameof(PersonResponse.DateOfBirth), "Date of birth"},
      {nameof(PersonResponse.PersonGender), "Gender"},
      {nameof(PersonResponse.Age), "Age"},
    };

    // get the list of persons
    List<PersonResponse> personsList = _personService.GetAllPersons();

    // filter the persons
    if(!string.IsNullOrEmpty(searchBy) && !string.IsNullOrEmpty(searchString)) {
      personsList = _personService.GetFilteredPersons(searchBy, searchString);
    }

    // sort the persons
    if(!string.IsNullOrEmpty(sortBy)) {
      personsList = _personService.GetSortedPersons(personsList, sortBy, sortOrder);
    }

    // populate the viewdata with the persons
    ViewData["persons"] = personsList;

    // persist the search criteria
    ViewData["CurrentSortBy"] = sortBy;
    ViewData["CurrentSearchBy"] = searchBy ?? "PersonName";
    ViewData["CurrentSearchString"] = searchString;
    ViewData["CurrentSortOrder"] = sortOrder;

    return View();
  }

  [HttpGet]
  [Route("[action]")]
  public IActionResult Create() {
    new SelectListItem(){
      Text="Tresten", Value="t-pain"
    };
    return View();
  }

  [HttpGet]
  [Route("[action]/{personId}")]
  public IActionResult Edit(Guid personId) {
    // get the person object from the person id
    PersonResponse? personResponse = _personService.GetPersonByPersonId(personId);
    return View(personResponse?.ToPersonUpdateRequest());
  }

  [HttpPost]
  [Route("[action]/{personId}")]
  public IActionResult Edit(PersonUpdateRequest personUpdateRequest) {
    // update the person
    _personService.UpdatePerson(personUpdateRequest);

    // load the index view
    return RedirectToAction("Index");
  }

  [HttpPost]
  [Route("create")]
  public IActionResult PersonCreated(PersonAddRequest person) {

    // The model validation was successful
    if(ModelState.IsValid) {
      // Add the person to the list of persons
      PersonResponse personResponse =  _personService.AddPerson(person);
      return RedirectToAction("Index", "Persons");
    }
    // there was an issue with the model validation
    else {
      // pass the errors list to the view
      ViewData["errors"] = ModelState.Values
        .SelectMany(v => v.Errors)
        .SelectMany(e => e.ErrorMessage)
        .ToList();

      return BadRequest(ModelState);
    }

  }

}