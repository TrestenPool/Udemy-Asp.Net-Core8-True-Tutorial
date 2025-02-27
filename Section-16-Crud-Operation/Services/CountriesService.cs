using Entities;
using ServiceContracts;

namespace Services;

public class CountriesService : ICountriesService{
  // private field
  private readonly List<Country> _countries;

  // constructor
  public CountriesService(bool initialize = true) {
    _countries = new List<Country>();

    // if initialize is true, then add some countries to the list
    if(initialize) {
      List<Country> countries = new List<Country>{
        new(){Name="USA"},
        new(){Name="Mexico"},
        new(){Name="Canada"},
        new(){Name="Brazil"}
      };
      _countries.AddRange(countries);
    }

  }

  public CountryResponse AddCountry(CountryAddRequest? countryAddRequest){

    // argument is null
    ArgumentNullException.ThrowIfNull(countryAddRequest);

    // countryname is null
    if( string.IsNullOrEmpty(countryAddRequest.CountryName) ){
      throw new ArgumentException($"{nameof(countryAddRequest.CountryName)} can't be null");
    }

    // convert to country
    Country country = countryAddRequest.ToCountry();

    // check if the country is already in the _countries list
    if(_countries.Any(c => c.Name == country.Name)){
      throw new ArgumentException($"{country.Name}:: you are trying to insert duplicate");
    }

    // add country object into country list
    _countries.Add(country);

    // send the country response back to the user
    return country.ToCountryResponse();
  }

  public List<CountryResponse> GetAllCountries(){
    return _countries.Select(c => c.ToCountryResponse()).ToList();
  }

  public CountryResponse? GetCountryByCountryId(Guid? countryId){
    Country? retrievedCountry = _countries.FirstOrDefault(c => c.CountryId == countryId);
    return retrievedCountry?.ToCountryResponse();
  }

}
