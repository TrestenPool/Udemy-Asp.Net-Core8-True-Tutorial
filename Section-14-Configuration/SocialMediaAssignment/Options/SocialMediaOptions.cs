namespace SocialMediaAssignment.Options;

public class SocialMediaOptions{
  public string? OnCall {get; set;} 
  public string? OnCallAnalyticsPowerBI {get; set;}
  public string? CADPowerBI {get; set;}
  public string? CDCJailBooking {get; set;}

  public override string ToString() {
    return string.Join(", ",
      GetType().GetProperties()
        .Select(p => $"{p.Name} = {p.GetValue(this, null)}")
    );
  }
}