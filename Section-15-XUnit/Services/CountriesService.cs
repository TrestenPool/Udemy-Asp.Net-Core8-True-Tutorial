using Entities;
using ServiceContracts;

namespace Services;

public class CountriesService : ICountriesService{
  // private field
  private readonly List<Country> _countries;

  // constructor
  public CountriesService() {
    _countries = new List<Country>();
  }

  // implemented interface
  public CountryResponse AddCountry(CountryAddRequest? countryAddRequest){

    ArgumentNullException.ThrowIfNull(countryAddRequest);

    if( string.IsNullOrEmpty(countryAddRequest.CountryName) ){
      throw new ArgumentException("CountryName can't be null");
    }

    Country country = countryAddRequest.ToCountry();

    return null;
  }

}
