namespace ServiceContracts;

public interface ICitiesService{

  // ID for the object
  Guid ServiceInstanceId {get; }

  List<string> GetCities();
}
