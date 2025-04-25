using Entities;
using ServiceContracts;

namespace Services;

public class CountriesService : ICountriesService{
  // fields
  private readonly PersonsDbContext _db;

  // constructor
  public CountriesService(PersonsDbContext personsDbContext) {
    _db = personsDbContext;
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
    if(_db.Countries.Any(c => c.Name == country.Name)){
      throw new ArgumentException($"{country.Name}:: you are trying to insert duplicate");
    }

    // add country object into country list
    _db.Countries.Add(country);
    _db.SaveChanges();

    // send the country response back to the user
    return country.ToCountryResponse();
  }

  public List<CountryResponse> GetAllCountries(){
    return _db.Countries.Select(c => c.ToCountryResponse()).ToList();
  }

  public CountryResponse? GetCountryByCountryId(Guid? countryId){
    Country? retrievedCountry = _db.Countries.FirstOrDefault(c => c.CountryId == countryId);
    return retrievedCountry?.ToCountryResponse();
  }

}
