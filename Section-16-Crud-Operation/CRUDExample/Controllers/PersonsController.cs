using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.Enums;
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

  [Route("persons/create")]
  [HttpGet]
  public IActionResult Create() {
    return View();
  }


}