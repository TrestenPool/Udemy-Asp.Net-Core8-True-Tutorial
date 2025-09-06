using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
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

  public async Task<int> UploadFromExcelFile(IFormFile formFile)
  {
    // create the memory stream to be used to return to the caller
    MemoryStream ms = new MemoryStream();

    // copy the file that was passed to the memory stream
    await formFile.CopyToAsync(ms);

    int countriesInserted = 0;

    using (ExcelPackage excel = new ExcelPackage(ms))
    {
      ExcelWorksheet worksheet = excel.Workbook.Worksheets["Countries"];
      int rowCount = worksheet.Dimension.Rows;

      // go through the rows and insert the country
      for (int row = 2; row <= rowCount; row++)
      {
        string? cellValue = Convert.ToString(worksheet.Cells[row, 1].Value);
        if (!string.IsNullOrEmpty(cellValue))
        {
          string CountryName = cellValue;

          // only insert if there is not a country with that name already
          if (!_db.Countries.Any(c => c.Name == CountryName))
          {
            Country countryToBeAdded = new Country() { Name = CountryName, CountryId = Guid.NewGuid() };
            _db.Countries.Add(countryToBeAdded);
            await _db.SaveChangesAsync();
            countriesInserted++;
          }
        }
      }

    }

    // return the number of countries inserted
    return countriesInserted;
  }
}
