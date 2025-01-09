using ServiceContracts;

namespace Services;

public class CitiesListService : ICitiesService, IDisposable{
  private Guid _serviceInstanceId  ;

  // Property
  public Guid ServiceInstanceId {
    get{
      return _serviceInstanceId;
    }
  }

  // List of strings
  private readonly List<string> Cities = new List<string> {
    "Bishop",
    "Banquete",
    "Falfurias",
    "London",
    "Corpus Christi Moody"
  };

  // constructor
  public CitiesListService() {
    // generate a guid
    this._serviceInstanceId = Guid.NewGuid();
    // open connection to db

  }


  // Method to return the List
  public List<string> GetCities() {
    return Cities;
  }

  // dispose
  public void Dispose(){
    // close the db connection
  }

}
