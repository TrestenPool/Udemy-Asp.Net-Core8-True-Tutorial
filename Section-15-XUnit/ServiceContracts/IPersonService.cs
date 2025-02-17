using ServiceContracts.Enums;

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

  /// <summary>
  /// Returns a list of persons that matches the searchString.
  /// </summary>
  /// <param name="searchBy">What the SearchString will filter on</param>
  /// <param name="searchString">The Search string that will be used to filter the persons. If searchString is null it will return all of the results</param>
  /// <returns></returns>
  List<PersonResponse> GetFilteredPersons(string searchBy, string? searchString);

  /// <summary>
  /// Takes in a list and sorts based on the sortBy and the sortOrder
  /// Primarily used for clicking on a column to return the list in the sorted order
  /// </summary>
  /// <param name="allPersons"></param>
  /// <param name="sortBy"></param>
  /// <param name="sortOrder"></param>
  /// <returns></returns>
  List<PersonResponse> GetSortedPersons(List<PersonResponse> allPersons, string sortBy, SortOrderEnum sortOrder);

  PersonResponse UpdatePerson(PersonUpdateRequest? personUpdateRequest);
}
