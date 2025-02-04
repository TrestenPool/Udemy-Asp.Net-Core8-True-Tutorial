using Entities;

namespace ServiceContracts;

public class PersonResponse{
  public Guid PersonId { get; init;}
  public string? PersonName { get; set; }
  public string? Email { get; set; }
  public string? Address { get; set; }
  public DateTime? DateOfBirth { get; set; }
  public Gender? PersonGender { get; set; }
  public string? Country { get; set; }
  public bool ReceiveNewsLetters { get; set; }

  public override bool Equals(object? obj){

    if(obj is PersonResponse personResponseArg) {
      return 
        this.PersonId == personResponseArg.PersonId &&
        this.PersonName == personResponseArg.PersonName &&
        this.Email == personResponseArg.Email &&
        this.Address == personResponseArg.Address &&
        this.DateOfBirth == personResponseArg.DateOfBirth &&
        this.PersonGender == personResponseArg.PersonGender &&
        this.Country == personResponseArg.Country &&
        this.ReceiveNewsLetters == personResponseArg.ReceiveNewsLetters;
    }
    return false;
  }

  public override int GetHashCode(){
    return base.GetHashCode();
  }
}

public static class PersonResponseExtensions {

  public static PersonResponse ToPersonResponse(this Person person){

    return new PersonResponse(){
      PersonId = person.PersonId,
      Address = person.Address,
      DateOfBirth = person.DateOfBirth,
      PersonGender = person.PersonGender,
      PersonName=person.PersonName,
      Email = person.Email,
      ReceiveNewsLetters = person.ReceiveNewsLetters,

    };

  }
  
}
