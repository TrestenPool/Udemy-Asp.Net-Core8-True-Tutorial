using System.ComponentModel.DataAnnotations;
using Entities;
using ServiceContracts;
using ServiceContracts.Enums;

namespace Services;

public class PersonService : IPersonService{
  private List<Person> _personsList;
  private readonly ICountriesService _countriesService;

  public PersonService() {
    _personsList = new List<Person>();
    _countriesService = new CountriesService();
  }

  private PersonResponse ConvertPersonToPersonResponse(Person person) {
    PersonResponse personResponse = person.ToPersonResponse();
    personResponse.Country = _countriesService.GetCountryByCountryId(person.CountryId)?.CountryName;
    return personResponse;
  }

  public PersonResponse AddPerson(PersonAddRequest? personAddRequest){
    // personaddrequest is null
    ArgumentNullException.ThrowIfNull(personAddRequest,nameof(personAddRequest));

    // Validation the properties on the arg personAddRequest
    ValidationHelper.ModelValidation(personAddRequest);

    // convert the personAddRequest to person
    Person person = personAddRequest.ToPerson();

    // add the person to the list
    _personsList.Add(person);

    // return the person response
    return ConvertPersonToPersonResponse(person);
  }

  public List<PersonResponse> GetAllPersons(){
    return _personsList.Select(p => p.ToPersonResponse()).ToList();
  }

  public PersonResponse? GetPersonByPersonId(Guid? personId){
    // throw null exc if arg is null
    ArgumentNullException.ThrowIfNull(personId,nameof(personId));
    
    // return the first personId that matches in the list or null if it can't find it
    return _personsList
      .Where(p => p.PersonId == personId)
      .Select(p => p.ToPersonResponse())
      .FirstOrDefault();
  }

  public List<PersonResponse> GetFilteredPersons(string searchBy, string? searchString){
    // get all of the persons
    List<PersonResponse> allPersons = GetAllPersons();

    // list that will contain only the matching persons
    List<PersonResponse> matchingPersons = allPersons;

    // the searchBy string is empty so return all persons
    if(string.IsNullOrEmpty(searchBy)){
      return matchingPersons;
    }

    switch(searchBy) {
      // Get all of the person Names where it contains the searchstring
      case nameof(Person.PersonName):
        return allPersons.Where(p => p.PersonName?.Contains(searchString!,StringComparison.OrdinalIgnoreCase) ?? false).ToList();

      case nameof(Person.PersonGender):
        return allPersons.Where(p => p.PersonGender.ToString() == searchString).ToList();

      case nameof(Person.DateOfBirth):
        return allPersons.Where(p => p.DateOfBirth?.ToString("dd MMMM yyyy").Contains(searchString!, StringComparison.OrdinalIgnoreCase) ?? false ).ToList();
      
      default:
        return allPersons;
    }


  }

  public List<PersonResponse> GetSortedPersons(List<PersonResponse> allPersons, string sortBy, SortOrderEnum sortOrder){

    // return original if no searchby was given
    if(string.IsNullOrEmpty(sortBy)) {
      return allPersons;
    }

    // list we will return to the user
    List<PersonResponse> sortedPersons = sortBy switch{
      nameof(Person.PersonName) => allPersons.OrderBy(p => p.PersonName).ToList(),
      nameof(Person.Email) => allPersons.OrderBy(p => p.Email).ToList(),
      nameof(Person.DateOfBirth) => allPersons.OrderBy(p => p.DateOfBirth).ToList(),
      nameof(Person.PersonGender) => allPersons.OrderBy(p => p.PersonGender).ToList(),
      _ => allPersons
    };

    // sort in descending order if necessary
    if(sortOrder == SortOrderEnum.Descending) {
      sortedPersons.Reverse();
    }

    return sortedPersons;
  }

  public PersonResponse UpdatePerson(PersonUpdateRequest? personUpdateRequest){
    throw new NotImplementedException();
  }
}
