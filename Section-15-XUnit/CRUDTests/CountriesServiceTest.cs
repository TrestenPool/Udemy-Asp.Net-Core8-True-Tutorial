using ServiceContracts;
using Services;

namespace CRUDTests;

public class CountriesServiceTest{
  // Properties
  private readonly ICountriesService _countriesService;

  // constructor
  public CountriesServiceTest() {
    _countriesService = new CountriesService();
  }

  // enum used for test
  public enum AddCountry {
    Null_CountryAddRequest,
    Null_CountryName,
    Duplicate_CountryName,
    Success_CountryAddRequest
  }

  // our test
  [Theory]
  [InlineData(AddCountry.Null_CountryAddRequest)]
  [InlineData(AddCountry.Null_CountryName)]
  [InlineData(AddCountry.Duplicate_CountryName)]
  [InlineData(AddCountry.Success_CountryAddRequest)]
  public void AddCountry_Test(AddCountry addCountryOption) {

    // Arrange
    CountryAddRequest? request;

    Type exeptionType = typeof(ArgumentException);

    // switch based off of inlinedata
    switch(addCountryOption) {

      case AddCountry.Null_CountryAddRequest:
        request = null;
        Assert.Throws<ArgumentNullException>(() => {
          _countriesService.AddCountry(request);
        });
        break;

      case AddCountry.Null_CountryName:
        request = new CountryAddRequest(){CountryName = null};
        Assert.Throws<ArgumentException>(() => {
          _countriesService.AddCountry(request);
        });
        break;

      case AddCountry.Duplicate_CountryName:
        request = new CountryAddRequest(){CountryName = "USA"};
        Assert.Throws<ArgumentException>(() => {
          _countriesService.AddCountry(request);
          _countriesService.AddCountry(request);
        });
        break;

      case AddCountry.Success_CountryAddRequest:
        request = new CountryAddRequest(){CountryName = "USA"};
        CountryResponse response = _countriesService.AddCountry(request);
        Assert.True(response.CountryId != Guid.Empty);
        break;

      default:
        break;
    }


  }
  
}