using System.ComponentModel.DataAnnotations;
using Entities;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using ServiceContracts.Enums;

namespace Services;

public class PersonService : IPersonService{
  // Properties
  private PersonsDbContext _db;
  ICountriesService _countriesService;

  // Constructor
  public PersonService(PersonsDbContext personsDbContext, ICountriesService countriesService) {
    _db = personsDbContext;
    _countriesService = countriesService;
  }

  // Private methods
  private PersonResponse ConvertPersonToPersonResponse(Person person) {
    PersonResponse personResponse = person.ToPersonResponse();
    var countryFound = _countriesService.GetCountryByCountryId(person.CountryId);
    personResponse.Country = countryFound?.CountryName;
    return personResponse;
  }

  // Public Methods
  public PersonResponse AddPerson(PersonAddRequest? personAddRequest){
    // personaddrequest is null
    ArgumentNullException.ThrowIfNull(personAddRequest,nameof(personAddRequest));

    // Validation the properties on the arg personAddRequest
    ValidationHelper.ModelValidation(personAddRequest);

    // convert the personAddRequest to person
    Person person = personAddRequest.ToPerson();

    // add the person to the list
    _db.Persons.Add(person);
    _db.SaveChanges();

    // return the person response
    return ConvertPersonToPersonResponse(person);
  }

  public List<PersonResponse> GetAllPersons(){
    return _db.Persons.ToList()
      .Select(p => ConvertPersonToPersonResponse(p)).ToList();
  }

  public PersonResponse? GetPersonByPersonId(Guid? personId){
    // throw null exc if arg is null
    ArgumentNullException.ThrowIfNull(personId,nameof(personId));
    
    // return the first personId that matches in the list or null if it can't find it
    return _db.Persons
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
      case nameof(PersonResponse.PersonName):
        return allPersons.Where(p => p.PersonName?.Contains(searchString!,StringComparison.OrdinalIgnoreCase) ?? false).ToList();

      case nameof(PersonResponse.Email):
        return allPersons.Where(p => p.Email?.Contains(searchString!,StringComparison.OrdinalIgnoreCase) ?? false).ToList();

      case nameof(PersonResponse.PersonGender):
        return allPersons.Where(p => string.Equals(p.PersonGender, searchString, StringComparison.OrdinalIgnoreCase) ).ToList();

      case nameof(PersonResponse.DateOfBirth):
        return allPersons.Where(p => p.DateOfBirth?.ToString("dd MMMM yyyy").Contains(searchString!, StringComparison.OrdinalIgnoreCase) ?? false ).ToList();

      case nameof(PersonResponse.Age):
        return allPersons.Where(p => string.Equals(p.Age.ToString(), searchString, StringComparison.OrdinalIgnoreCase)).ToList();
      
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
      nameof(PersonResponse.PersonName) => allPersons.OrderBy(p => p.PersonName).ToList(),
      nameof(PersonResponse.Email) => allPersons.OrderBy(p => p.Email).ToList(),
      nameof(PersonResponse.DateOfBirth) => allPersons.OrderBy(p => p.DateOfBirth).ToList(),
      nameof(PersonResponse.PersonGender) => allPersons.OrderBy(p => p.PersonGender).ToList(),
      nameof(PersonResponse.Age) => allPersons.OrderBy(p => p.Age).ToList(),
      _ => allPersons
    };

    // sort in descending order if necessary
    if(sortOrder == SortOrderEnum.Descending) {
      sortedPersons.Reverse();
    }

    return sortedPersons;
  }

  public PersonResponse UpdatePerson(PersonUpdateRequest? personUpdateRequest){
    // throw an error if the arg is null
    ArgumentNullException.ThrowIfNull(personUpdateRequest);

    // validate personUpdateRequest
    ValidationHelper.ModelValidation(personUpdateRequest);

    // grab the person from the list
    Person? personToUpdate = _db.Persons.FirstOrDefault(p => p.PersonId == personUpdateRequest.PersonId);

    // the person by that id was not found
    if(personToUpdate == null) {
      throw new ArgumentException($"No person found with {personUpdateRequest.PersonId}");
    }

    // update all of the properties
    personToUpdate.PersonName = personUpdateRequest.PersonName;
    personToUpdate.Email = personUpdateRequest.Email;
    personToUpdate.Address = personUpdateRequest.Address;
    personToUpdate.DateOfBirth = personUpdateRequest.DateOfBirth;
    personToUpdate.PersonGender = personUpdateRequest.PersonGender;
    personToUpdate.CountryId = personUpdateRequest.CountryId;
    personToUpdate.ReceiveNewsLetters = personUpdateRequest.ReceiveNewsLetters;

    // update the persons
    _db.Persons.Update(personToUpdate);

    // save the changes in the db
    _db.SaveChanges();

    // return the object to the user
    return personToUpdate.ToPersonResponse();
  }

  public bool DeletePerson(Guid? personId){
    // personid is null throw exceptionGender
    ArgumentNullException.ThrowIfNull(personId);

    // get the person from the list
    Person? personToDelete = _db.Persons.FirstOrDefault(p => p.PersonId == personId);


    if(personToDelete == null) {
      return false;
    }

    var result = _db.Persons.Remove(personToDelete);
    _db.SaveChanges();

    return true;
  }
}
