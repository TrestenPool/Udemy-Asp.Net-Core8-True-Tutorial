using System.ComponentModel.DataAnnotations;

namespace Entities;

public class Person{
  [Key]
  public Guid PersonId { get; } = Guid.NewGuid();
  [StringLength(40)]
  public string? PersonName { get; set; }
  public string? Email { get; set; }
  [StringLength(200)]
  public string? Address { get; set; }
  public DateTime? DateOfBirth { get; set; }
  public Gender? PersonGender { get; set; }
  public Guid? CountryId { get; set; }
  public bool ReceiveNewsLetters { get; set; }
}