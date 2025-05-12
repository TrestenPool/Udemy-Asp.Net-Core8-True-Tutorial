using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities;

public class Person{
  [Key]
  public Guid PersonId { get; set; }

  [StringLength(40)]
  public string? PersonName { get; set; }

  public string? Email { get; set; }

  [StringLength(200)]
  public string? Address { get; set; }

  public DateTime? DateOfBirth { get; set; }

  [StringLength(10)] //nvarchar(100)
  public string? PersonGender { get; set; }

  public Guid? CountryId { get; set; }

  public bool ReceiveNewsLetters { get; set; }

  public string? TIN {get; set;}

  // [ForeignKey("CountryId")]
  public Country? Country {get; set;}
}