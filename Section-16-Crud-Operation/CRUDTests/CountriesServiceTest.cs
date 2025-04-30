using Entities;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using Services;

namespace CRUDTests;

public class CountriesServiceTest{
  // Properties
  private readonly ICountriesService _countriesService;

  // constructor
  public CountriesServiceTest() {
    _countriesService = new CountriesService(
      new PersonsDbContext(new 
        DbContextOptionsBuilder<PersonsDbContext>().Options
    ));
  }

  // enum used for test
  public enum AddCountry {
    Null_CountryAddRequest,
    Null_CountryName,
    Duplicate_CountryName,
    Success_CountryAddRequest
  }

  #region AddCountry
  [Theory]
  [InlineData(AddCountry.Null_CountryAddRequest)]
  [InlineData(AddCountry.Null_CountryName)]
  [InlineData(AddCountry.Duplicate_CountryName)]
  [InlineData(AddCountry.Success_CountryAddRequest)]
  public void AddCountry_Test(AddCountry addCountryOption) {

    // Arrange
    CountryAddRequest? request;

    // Act & Assert
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
  #endregion


  #region GetAllCountries
  public enum GetAllCountriesEnum {
    EmptyList,
    AddFewCountries,
    AddSingleCountry
  }

  [Theory]
  [InlineData(GetAllCountriesEnum.EmptyList)]
  public void GetAllCountries_Test(GetAllCountriesEnum option) {
    List<CountryResponse> countriesResponseList;

    switch(option) {

      case GetAllCountriesEnum.EmptyList:
        countriesResponseList = _countriesService.GetAllCountries();
        Assert.Empty(countriesResponseList);
      break;

      case GetAllCountriesEnum.AddFewCountries:
        List<CountryAddRequest> countries_to_add = new List<CountryAddRequest>(){
          new CountryAddRequest(){CountryName = "USA"},
          new CountryAddRequest(){CountryName = "UK"}
        };

        List<CountryResponse> expected_countries_response = new List<CountryResponse>();
        
        // add the elements in the request list
        foreach(var request in countries_to_add) {
          expected_countries_response.Add( _countriesService.AddCountry(request) );
        }

        // Gets the countries
        countriesResponseList = _countriesService.GetAllCountries();

        Assert.Equal(countriesResponseList, expected_countries_response);
      break;

      case GetAllCountriesEnum.AddSingleCountry:
        // Arrange
        CountryAddRequest? countryAddRequest = new CountryAddRequest(){CountryName="Japan"};
        CountryResponse response;

        // Act
        response = _countriesService.AddCountry(countryAddRequest);
        countriesResponseList = _countriesService.GetAllCountries();

        // Assert
        Assert.True(response.CountryId != Guid.Empty);
        Assert.Contains(response, countriesResponseList);

      break;

    }
  }
  #endregion


  #region GetCountryByCountryId
  public enum GetCountryByCountryId {
    Null_CountryId,
    Valid_CountryId,
    Invalid_CountryId,
  }


  [Theory]
  [InlineData(GetCountryByCountryId.Null_CountryId)]
  [InlineData(GetCountryByCountryId.Valid_CountryId)]
  [InlineData(GetCountryByCountryId.Invalid_CountryId)]
  public void GetCountryById_Test(GetCountryByCountryId option) {
    switch(option) {

      case GetCountryByCountryId.Null_CountryId:
      // get country with null country id
      var response = _countriesService.GetCountryByCountryId(null);
      // should be null
      Assert.Null(response);
      break;

      case GetCountryByCountryId.Valid_CountryId:
      // add new country
      var countryAdded = _countriesService.AddCountry(
        new CountryAddRequest(){CountryName="USA"}
      );
      // attempt to retrieve the country we just added
      var countryReceived = _countriesService.GetCountryByCountryId(countryAdded.CountryId);
      // assert the country we added is the one we just retrieved
      Assert.Equal(countryReceived, countryAdded);
      break;

      case GetCountryByCountryId.Invalid_CountryId:
      var countryRetrieved = _countriesService.GetCountryByCountryId(new Guid());
      Assert.Null(countryRetrieved);
      break;
    }
  }
  #endregion



  
}