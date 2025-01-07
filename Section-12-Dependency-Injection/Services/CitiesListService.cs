namespace Services;

public class CitiesListService{

  private readonly List<string> Cities = new List<string> {
    "Bishop",
    "Banquete",
    "Falfurias",
    "London",
    "Corpus Christi Moody"
  };

  public List<string> GetCitiesList() {
    return Cities;
  }

}
