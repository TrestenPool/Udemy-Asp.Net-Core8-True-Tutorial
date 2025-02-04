using Entities;

namespace ServiceContracts;

public class PersonAddRequest{
  public Guid PersonId { get; } = Guid.NewGuid();
  public string? PersonName { get; set; }
  public string? Email { get; set; }
  public string? Address { get; set; }
  public DateTime? DateOfBirth { get; set; }
  public Gender? PersonGender { get; set; }
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


