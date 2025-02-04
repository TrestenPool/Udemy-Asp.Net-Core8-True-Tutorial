using Entities;
using ServiceContracts;

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

    // personname is null
    if(personAddRequest.PersonName == null) {
      throw new ArgumentException(nameof(personAddRequest.PersonName));
    }

    // convert the personaddreques to person
    Person person = personAddRequest.ToPerson();

    // add the person to the list
    _personsList.Add(person);

    // return the person response
    return ConvertPersonToPersonResponse(person);
  }

  public List<PersonResponse> GetAllPersons(){
    return _personsList.Select(p => p.ToPersonResponse()).ToList();
  }

}
