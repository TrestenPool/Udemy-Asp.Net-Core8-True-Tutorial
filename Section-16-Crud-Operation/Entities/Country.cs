namespace Entities;

/// <summary>
/// Domain model for Country
/// </summary>
public class Country{
  public Guid CountryId { get; } = Guid.NewGuid();
  public string? Name { get; set; }
}
