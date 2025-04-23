using Entities;
using ServiceContracts;

namespace Services;

public class CountriesService : ICountriesService{
  // fields
  private readonly List<Country> _countries;

  // constructor
  public CountriesService(bool initialize = true) {
    // initialize
    _countries = new List<Country>();

    _countries.Add(
      new Country(){Name="USA", CountryId=new Guid("e170468d-a1d2-4f2b-b1b0-ff4df8dac50d")}
    );
    _countries.Add(
      new Country(){Name="Russia", CountryId=new Guid("fc4ad2d2-8c12-4eb1-840d-2e8d9f0687eb")}
    );
    _countries.Add(
      new Country(){Name="China", CountryId=new Guid("84e34617-ea13-4025-af68-e6a027865c76")}
    );
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
