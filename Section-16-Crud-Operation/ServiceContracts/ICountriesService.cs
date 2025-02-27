﻿namespace ServiceContracts;

/// <summary>
/// Represents business logic for manipulating country entity
/// </summary>
public interface ICountriesService{
  /// <summary>
  /// Adds a country object to the list of countries
  /// </summary>
  /// <param name="countryAddRequest">Country object to add</param>
  /// <returns>Returns the country object after adding it (including newly generated country id)</returns>
  CountryResponse AddCountry(CountryAddRequest? countryAddRequest);

  /// <summary>
  /// Returns all countries from list
  /// </summary>
  /// <returns></returns>
  List<CountryResponse> GetAllCountries();

  /// <summary>
  /// Returns country object based on given country id
  /// </summary>
  /// <param name="countryId">CountryID (guid) to search</param>
  /// <returns>Matching Country as countryResponse object</returns>
  CountryResponse? GetCountryByCountryId(Guid? countryId);
}
