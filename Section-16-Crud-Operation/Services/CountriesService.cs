using Entities;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;

namespace Services;

public class CountriesService : ICountriesService{
  // fields
  private readonly PersonsDbContext _db;

  // constructor
  public CountriesService(PersonsDbContext personsDbContext) {
    _db = personsDbContext;
  }

  public async Task<CountryResponse> AddCountry(CountryAddRequest? countryAddRequest){

    // argument is null
    ArgumentNullException.ThrowIfNull(countryAddRequest);

    // countryname is null
    if( string.IsNullOrEmpty(countryAddRequest.CountryName) ){
      throw new ArgumentException($"{nameof(countryAddRequest.CountryName)} can't be null");
    }

    // convert to country
    Country country = countryAddRequest.ToCountry();

    // check if the country is already in the _countries list
    if(await _db.Countries.AnyAsync(c => c.Name == country.Name)){
      throw new ArgumentException($"{country.Name}:: you are trying to insert duplicate");
    }

    // add country object into country list
    _db.Countries.Add(country);
    await _db.SaveChangesAsync();

    // send the country response back to the user
    return country.ToCountryResponse();
  }

  public async Task<List<CountryResponse>> GetAllCountries(){
    return await _db.Countries.Select(c => c.ToCountryResponse()).ToListAsync();
  }

  public async Task<CountryResponse?> GetCountryByCountryId(Guid? countryId){
    Country? retrievedCountry = await _db.Countries.FirstOrDefaultAsync(c => c.CountryId == countryId);
    return retrievedCountry?.ToCountryResponse();
  }

}
