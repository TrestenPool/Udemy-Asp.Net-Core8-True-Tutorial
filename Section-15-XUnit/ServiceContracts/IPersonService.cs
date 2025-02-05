namespace ServiceContracts;

public interface IPersonService{
  /// <summary>
  /// Adds a new person to the persons list
  /// </summary>
  /// <param name="personAddRequest">The person you want to add</param>
  /// <returns>Returns a PersonResponse of the person you just added</returns>
  PersonResponse AddPerson(PersonAddRequest? personAddRequest);

  /// <summary>
  /// Gets a list of all of the persons in the persons list
  /// </summary>
  /// <returns>A list of PersonResponse</returns>
  List<PersonResponse> GetAllPersons();

  /// <summary>
  /// Gets a person given the personid
  /// </summary>
  /// <param name="personId"></param>
  /// <returns></returns>
  PersonResponse? GetPersonByPersonId(Guid? personId);
}
