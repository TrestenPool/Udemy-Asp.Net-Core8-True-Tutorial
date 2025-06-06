using System.Runtime.InteropServices.Marshalling;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceContracts;
using ServiceContracts.Enums;
using Services;
using Rotativa.AspNetCore;

[Route("[controller]")]
public class PersonsController : Controller
{
  // private fields
  private readonly IPersonService _personService;
  private readonly ICountriesService _countriesService;

  // constructor
  public PersonsController(IPersonService personService, ICountriesService countriesService)
  {
    _personService = personService;
    _countriesService = countriesService;
  }

  [Route("")]
  [Route("[action]")]
  public async Task<IActionResult> Index(
    [FromQuery] string searchBy,
    [FromQuery] string searchString,
    [FromQuery] string sortBy = "PersonName",
    [FromQuery] SortOrderEnum sortOrder = SortOrderEnum.Ascending
    )
  {

    // Add the alerts to the top of the page
    if (TempData["SuccessAlert"] != null)
    {
      ViewData["SuccessAlert"] = TempData["SuccessAlert"];
    }
    if (TempData["ErrorAlert"] != null)
    {
      ViewData["ErrorAlert"] = TempData["ErrorAlert"];
    }

    // populate options to search by
    ViewData["SearchFields"] = new Dictionary<string, string>(){
      {nameof(PersonResponse.PersonName), "Name"},
      {nameof(PersonResponse.Email), "Email"},
      {nameof(PersonResponse.DateOfBirth), "Date of birth"},
      {nameof(PersonResponse.PersonGender), "Gender"},
      {nameof(PersonResponse.Age), "Age"},
    };

    // get the list of persons
    List<PersonResponse> personsList = await _personService.GetAllPersons();

    // filter the persons
    if (!string.IsNullOrEmpty(searchBy) && !string.IsNullOrEmpty(searchString))
    {
      personsList = await _personService.GetFilteredPersons(searchBy, searchString);
    }

    // sort the persons
    if (!string.IsNullOrEmpty(sortBy))
    {
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
  public IActionResult Create()
  {
    new SelectListItem()
    {
      Text = "Tresten",
      Value = "t-pain"
    };
    return View();
  }

  [HttpGet]
  [Route("[action]/{personId}")]
  public async Task<IActionResult> Edit(Guid personId)
  {
    // get the person object from the person id
    PersonResponse? personResponse = await _personService.GetPersonByPersonId(personId);

    // person id was not found, redirect to index page
    if (personResponse == null)
    {
      return RedirectToAction("Index");
    }

    List<CountryResponse> countries = await _countriesService.GetAllCountries();
    ViewBag.Countries = countries.Select(temp =>
    new SelectListItem() { Text = temp.CountryName, Value = temp.CountryId.ToString() });

    // go to the edit view
    return View(personResponse?.ToPersonUpdateRequest());
  }

  [HttpPost]
  [Route("[action]/{personId}")]
  public async Task<IActionResult> Edit(PersonUpdateRequest personUpdateRequest)
  {

    // update the the model since validation passed
    if (ModelState.IsValid)
    {
      // for the service to update the person
      await _personService.UpdatePerson(personUpdateRequest);
      TempData["SuccessAlert"] = $"Success updating {personUpdateRequest.PersonName}";
    }
    else
    {
      // load the viewdata with errors
      TempData["ErrorAlert"] = string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
    }

    // load the index view
    return RedirectToAction("Index");
  }

  [HttpPost]
  [Route("create")]
  public async Task<IActionResult> PersonCreated(PersonAddRequest person)
  {

    // The model validation was successful
    if (ModelState.IsValid)
    {
      // Add the person to the list of persons
      PersonResponse personResponse = await _personService.AddPerson(person);
      TempData["SuccessAlert"] = $"Successfully created person: {personResponse.PersonName}";
      return RedirectToAction("Index", "Persons");
    }
    // there was an issue with the model validation
    else
    {
      // pass the errors list to the view
      ViewData["errors"] = ModelState.Values
        .SelectMany(v => v.Errors)
        .SelectMany(e => e.ErrorMessage)
        .ToList();

      return BadRequest(ModelState);
    }

  }

  [HttpGet]
  [Route("[action]/{personId}")]
  public async Task<IActionResult> Delete(Guid personId)
  {
    // Get the person object
    PersonResponse? personResponse = await _personService.GetPersonByPersonId(personId);

    if (personResponse == null)
    {
      TempData["ErrorAlert"] = $"Unable to find person with id {personId}";
      return RedirectToAction("Index");
    }

    return View(personResponse);
  }

  [HttpPost]
  [Route("[action]/{personId}")]
  public async Task<IActionResult> DeletePerson(Guid personId)
  {
    bool result = await _personService.DeletePerson(personId);

    if (!result)
    {
      TempData["ErrorAlert"] = $"There was an issue deleting person with id: {personId}";
    }
    else
    {
      TempData["SuccessAlert"] = $"Successfully deleted person with id: {personId}";
    }

    return RedirectToAction("Index");
  }

  [Route("[action]")]
  public async Task<IActionResult> PersonsPDF()
  {
    // get list of persons
    List<PersonResponse> persons = await _personService.GetAllPersons();

    return new ViewAsPdf("PersonsPDF", persons, ViewData)
    {
      PageMargins = new Rotativa.AspNetCore.Options.Margins(20, 20, 20, 20),
      PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape,
      FileName = "AllPersons.pdf"
    };
  }

  [Route("[action]")]
  public async Task<IActionResult> PersonsCSV()
  {
    // get the persons csv in memory
    var csvfile = await _personService.GetPersonsCSV();
    return File(csvfile, "application/octet-stream", "persons.csv");
  }

}
