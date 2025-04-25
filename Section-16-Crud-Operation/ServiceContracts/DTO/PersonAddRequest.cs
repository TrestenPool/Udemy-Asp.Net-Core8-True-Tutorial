using System.ComponentModel.DataAnnotations;
using Entities;

namespace ServiceContracts;

public class PersonAddRequest{
  public Guid PersonId { get; } = Guid.NewGuid();
  [Required(ErrorMessage = "Person Name can't be blank")]
  [DataType(DataType.Text)]
  public string? PersonName { get; set; }

  [Required(ErrorMessage = "Email can't be blank")]
  [EmailAddress]
  [DataType(DataType.EmailAddress)]
  public string? Email { get; set; }
  public string? Address { get; set; }
  public DateTime? DateOfBirth { get; set; }
  public string? PersonGender { get; set; }
  public Guid? CountryId { get; set; }
  public bool ReceiveNewsLetters { get; set; }

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


