using Entities;

namespace ServiceContracts;

/// <summary>
/// DTO class tha is used as return type for most of CountriesService methods
/// </summary>
public class CountryResponse{
  public Guid CountryId { get; set; }
  public string? CountryName { get; set; }

  public override bool Equals(object? obj){
    if(obj == null) {
      return false;
    }

    if(obj.GetType() != typeof(CountryResponse)) {
      return false;
    }

    CountryResponse argument = (CountryResponse)obj;
    return 
      this.CountryId == argument.CountryId && 
      this.CountryName == argument.CountryName;
  }

  public override int GetHashCode(){
    return base.GetHashCode();
  }
}

public static class CountryExtensions {

  /// <summary>
  /// Returns a Country from a CountryResponse
  /// </summary>
  /// <param name="country"></param>
  /// <returns></returns>
  public static CountryResponse ToCountryResponse(this Country country) {  
    return new CountryResponse(){
      CountryId = country.CountryId,
      CountryName = country.Name
    };
  }

}


