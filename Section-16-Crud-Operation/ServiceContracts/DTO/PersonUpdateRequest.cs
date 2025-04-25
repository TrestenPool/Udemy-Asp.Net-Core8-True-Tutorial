using System.ComponentModel.DataAnnotations;
using Entities;

namespace ServiceContracts;

/// <summary>
/// Represents the DTO class that contains the person details to update
/// </summary>
public class PersonUpdateRequest{
  public Guid PersonId { get; init;} 
  [Required(ErrorMessage = "Person Name can't be blank")]
  public string? PersonName { get; set; }
  [Required(ErrorMessage = "Email can't be blank")]
  [EmailAddress]
  public string? Email { get; set; }
  public string? Address { get; set; }
  public DateTime? DateOfBirth { get; set; }
  public string? PersonGender { get; set; }
  public Guid? CountryId { get; set; }
  public string? Country { get; set; }
  public bool ReceiveNewsLetters { get; set; }

  /// <summary>
  /// Converts the PersonUpdateRequest to a Person
  /// </summary>
  /// <returns></returns>
  public Person ToPerson() {
    return new Person() {
      PersonName = PersonName,
      Email = Email,
      DateOfBirth = DateOfBirth,
      PersonGender = PersonGender,
      Address = Address,
      CountryId = CountryId,
      ReceiveNewsLetters = ReceiveNewsLetters
    };
  }


}
