using ServiceContracts;

namespace Services;

public class CitiesListService : ICitiesService{

  private readonly List<string> Cities = new List<string> {
    "Bishop",
    "Banquete",
    "Falfurias",
    "London",
    "Corpus Christi Moody"
  };

  public List<string> GetCities() {
    return Cities;
  }
}
