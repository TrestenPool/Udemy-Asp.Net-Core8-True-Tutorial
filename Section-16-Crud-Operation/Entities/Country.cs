using System.ComponentModel.DataAnnotations;

namespace Entities;

/// <summary>
/// Domain model for Country
/// </summary>
public class Country{
  // gets initialized
  [Key]
  public Guid CountryId { get; init; }
  public string? Name { get; set; }
  public ICollection<Person>? Persons {get; set;}
}
