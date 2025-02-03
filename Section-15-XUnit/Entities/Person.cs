namespace Entities;

public class Person{
  public Guid PersonId { get; set; }
  public string? PersonName { get; set; }
  public string? Email { get; set; }
  public string? Address { get; set; }
  public DateTime? DateOfBirth { get; set; }
  public Gender? PersonGender { get; set; }
  public Guid? CountryId { get; set; }
  public bool ReceiveNewsLetters { get; set; }
}