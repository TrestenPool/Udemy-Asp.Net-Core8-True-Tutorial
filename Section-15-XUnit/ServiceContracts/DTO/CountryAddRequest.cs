using Entities;

namespace ServiceContracts;

/// <summary>
/// DTO Class for adding a new country
/// </summary>
public class CountryAddRequest{

  // Properties
  public string? CountryName { get; set; }

  // methods
  // convert CountryAddRequest to Country object
  public Country ToCountry() {
    return new Country(){Name = CountryName};
  }

}
