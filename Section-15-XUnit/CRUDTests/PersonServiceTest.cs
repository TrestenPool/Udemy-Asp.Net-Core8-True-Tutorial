using Entities;
using ServiceContracts;
using ServiceContracts.Enums;
using Services;
using Xunit.Abstractions;

namespace CRUDtests;

public class PersonServiceTest {
  // property
  private readonly IPersonService _personService;
  private readonly ITestOutputHelper _outputHelper;

  // constructor
  public PersonServiceTest(ITestOutputHelper testOutputHelper) {
    _personService = new PersonService();
    _outputHelper = testOutputHelper;
  }

  public enum AddPersonTests {
    NullPerson,
    PersonNameNull,
    ValidPerson
  }

  [Theory]
  [InlineData(AddPersonTests.NullPerson)]
  [InlineData(AddPersonTests.PersonNameNull)]
  [InlineData(AddPersonTests.ValidPerson)]
  public void AddPersonTest(AddPersonTests addPersonOption) {
    PersonAddRequest? personRequest;
    PersonResponse? personResponse;
    List<PersonResponse> persons_list;
    switch(addPersonOption) {

      case AddPersonTests.NullPerson:
      Assert.Throws<ArgumentNullException>(() => {
        _personService.AddPerson(null);
      });
      break;

      case AddPersonTests.PersonNameNull:
      personRequest = new PersonAddRequest(){PersonName = null};
      Assert.Throws<ArgumentException>(() => {
        _personService.AddPerson(personRequest);
      });
      break;

      case AddPersonTests.ValidPerson:
      // arrange
      personRequest = new PersonAddRequest(){
        PersonName = "Tresten",
        Email="trestenpool@gmail.com",
        PersonGender = Gender.Male,
        Address="1150 Carnaby St",
        DateOfBirth=new DateTime(1999,1,29)
      };

      // act
      personResponse = _personService.AddPerson(personRequest);
      persons_list = _personService.GetAllPersons();

      // assert
      Assert.True(personResponse.PersonId != Guid.Empty);
      Assert.Contains(personResponse, persons_list);
      break;
    }
  }


  public enum PersonIdOptions {
    NullPersonId,
    ValidPersonId,
    InvalidPersonId
  }

  [Theory]
  [InlineData(PersonIdOptions.NullPersonId)]
  [InlineData(PersonIdOptions.ValidPersonId)]
  [InlineData(PersonIdOptions.InvalidPersonId)]
  public void GetPersonByPersonIdTest(PersonIdOptions personIdOption) {
    PersonAddRequest? personAddRequest;
    PersonResponse? personResponse;

    switch(personIdOption) {

      case PersonIdOptions.NullPersonId:
        Assert.Throws<ArgumentNullException>(() => {
          _personService.GetPersonByPersonId(null);
        });
        break;

      case PersonIdOptions.ValidPersonId:
        personAddRequest = new PersonAddRequest(){
          PersonName="Tresten",
          Email="tresten_email@gmail.com",
          Address="412 North Henderson"
        };
        personResponse = _personService.AddPerson(personAddRequest);
        PersonResponse? actualPerson = _personService.GetPersonByPersonId(personResponse.PersonId);
        Assert.True(personResponse.PersonId == actualPerson?.PersonId);
        break;

      case PersonIdOptions.InvalidPersonId:
        personResponse = _personService.GetPersonByPersonId(Guid.NewGuid());
        Assert.Null(personResponse);
        break;
    }

  }

  public enum AllPersonsOptions {
    NoPersons,
    SomePersons
  }

  [Theory]
  [InlineData(AllPersonsOptions.NoPersons)]
  [InlineData(AllPersonsOptions.SomePersons)]
  public void AllPersonsTest(AllPersonsOptions options) {
    List<PersonResponse> list_of_persons;
    switch(options) {
      case AllPersonsOptions.NoPersons:
        list_of_persons = _personService.GetAllPersons();
        Assert.Empty(list_of_persons);
        break;

      case AllPersonsOptions.SomePersons:
        // add our persons
        _personService.AddPerson(
          new PersonAddRequest(){PersonName="Tresten",Address="110 Notyourbusiness",Email="Tresten@yahoo.com"}
        );
        _personService.AddPerson(
          new PersonAddRequest(){PersonName="John",Address="4513 GetOffMyLawn St",Email="John@hotmail.com"}
        );
        // get the list of persons
        list_of_persons = _personService.GetAllPersons();
        // make sure the persons we added are in the collection
        Assert.Collection(list_of_persons, 
          p => {
            Assert.Equal("Tresten", p.PersonName);
            _outputHelper.WriteLine("=====\n\nHI MATE\n\n====");
          },
          p => {
            Assert.Equal("John", p.PersonName);
          }
        );
        break;

    }
  }


public enum FilteredPersonsOptions {
  SomePersons,
  NoPersons
}
[Theory]
[InlineData(FilteredPersonsOptions.SomePersons)]
[InlineData(FilteredPersonsOptions.NoPersons)]
public void GetFilteredPersonsTest(FilteredPersonsOptions option) {
  switch(option) {
    case FilteredPersonsOptions.SomePersons:
      List<PersonResponse> list_response_persons_added = AddToPersons();
      List<PersonResponse> actual_list = _personService.GetFilteredPersons(nameof(Person.PersonName), "Tresten");
      Assert.Collection(actual_list,
        p => Assert.True(p.PersonName == "Tresten")
      );
      break;

    case FilteredPersonsOptions.NoPersons:
      break;
  }
}

public enum SortedPersonsEnum {
  AscendingPersonName,
  DescendingPersonName
}
[Theory]
[InlineData(SortedPersonsEnum.AscendingPersonName)]
[InlineData(SortedPersonsEnum.DescendingPersonName)]
public void GetSortedPersonsTest(SortedPersonsEnum options) {

  // variable to be used in switch branch
  List<PersonResponse> expected;
  List<PersonResponse> actual;

  switch(options) {
    case SortedPersonsEnum.AscendingPersonName:
      expected = AddToPersons();
      expected = expected.OrderBy(p => p.PersonName).ToList();
      actual = _personService.GetSortedPersons(expected,nameof(Person.PersonName),SortOrderEnum.Ascending);
      Assert.Equal(expected, actual);
      break;

    case SortedPersonsEnum.DescendingPersonName:
      expected = AddToPersons();
      expected = expected
        .OrderBy(p => p.PersonName)
        .Reverse()
        .ToList();
      actual = _personService.GetSortedPersons(
        expected,
        nameof(Person.PersonName),
        SortOrderEnum.Descending);
      Assert.Equal(expected, actual);
      break;

    default:
      break;
  }
}


public enum UpdatePersonEnum {
  NullPersonUpdateRequest,
  ValidPersonUpdateRequest
}
[Theory]
[InlineData(UpdatePersonEnum.NullPersonUpdateRequest)]
[InlineData(UpdatePersonEnum.ValidPersonUpdateRequest)]
public void UpdatePersonTest(UpdatePersonEnum option) {
  switch(option) {
    case UpdatePersonEnum.NullPersonUpdateRequest:
      break;
  }
}




/// <summary>
/// Returns a List<PersonResponse> of all of the persons that have been added
/// </summary>
/// <returns></returns>
private List<PersonResponse> AddToPersons(){
  List<PersonResponse> list_persons_added = new();

  list_persons_added.Add(_personService.AddPerson(
    new PersonAddRequest(){
      PersonName="Tresten",Address="1120 Rex Dr",Email="trestenp@gmail.com"
    }
  ));

  list_persons_added.Add(_personService.AddPerson(
    new PersonAddRequest(){
      PersonName="Yahan",Address="322 mulburry dr",Email="yahan@yahoo.com"
    }
  ));

  list_persons_added.Add(_personService.AddPerson(
    new PersonAddRequest(){
      PersonName="Dave",Address="1243 Qwerty St",Email="DaveB@yahoo.com"
    }
  ));

  return list_persons_added;
}

}