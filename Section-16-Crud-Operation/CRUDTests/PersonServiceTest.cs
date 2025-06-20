using Entities;
using Microsoft.EntityFrameworkCore;
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

    ICountriesService countriesService = new CountriesService(new PersonsDbContext(
      new DbContextOptionsBuilder<PersonsDbContext>().Options
    ));

    _personService = new PersonService(new PersonsDbContext(
      new DbContextOptionsBuilder<PersonsDbContext>().Options
    ), countriesService);
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
  public async Task AddPersonTest(AddPersonTests addPersonOption) {
    PersonAddRequest? personRequest;
    PersonResponse? personResponse;
    List<PersonResponse> persons_list;
    switch(addPersonOption) {

      case AddPersonTests.NullPerson:
      await Assert.ThrowsAsync<ArgumentNullException>(async () => {
        await _personService.AddPerson(null);
      });
      break;

      case AddPersonTests.PersonNameNull:
      personRequest = new PersonAddRequest(){PersonName = null};
      await Assert.ThrowsAsync<ArgumentException>(async () => {
        await _personService.AddPerson(personRequest);
      });
      break;

      case AddPersonTests.ValidPerson:
      // arrange
      personRequest = new PersonAddRequest(){
        PersonName = "Tresten",
        Email="trestenpool@gmail.com",
        PersonGender = "Male",
        Address="1150 Carnaby St",
        DateOfBirth=new DateTime(1999,1,29)
      };

      // act
      personResponse = await _personService.AddPerson(personRequest);
      persons_list = await _personService.GetAllPersons();

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
  public async Task GetPersonByPersonIdTest(PersonIdOptions personIdOption) {
    PersonAddRequest? personAddRequest;
    PersonResponse? personResponse;

    switch(personIdOption) {

      case PersonIdOptions.NullPersonId:
        await Assert.ThrowsAsync<ArgumentNullException>(async () => {
          await _personService.GetPersonByPersonId(null);
        });
        break;

      case PersonIdOptions.ValidPersonId:
        personAddRequest = new PersonAddRequest(){
          PersonName="Tresten",
          Email="tresten_email@gmail.com",
          Address="412 North Henderson"
        };
        personResponse = await _personService.AddPerson(personAddRequest);
        PersonResponse? actualPerson = await _personService.GetPersonByPersonId(personResponse.PersonId);
        Assert.True(personResponse.PersonId == actualPerson?.PersonId);
        break;

      case PersonIdOptions.InvalidPersonId:
        personResponse = await _personService.GetPersonByPersonId(Guid.NewGuid());
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
  public async Task AllPersonsTest(AllPersonsOptions options) {
    List<PersonResponse> list_of_persons;
    switch(options) {
      case AllPersonsOptions.NoPersons:
        list_of_persons = await _personService.GetAllPersons();
        Assert.Empty(list_of_persons);
        break;

      case AllPersonsOptions.SomePersons:
        // add our persons
        await _personService.AddPerson(
          new PersonAddRequest(){PersonName="Tresten",Address="110 Notyourbusiness",Email="Tresten@yahoo.com"}
        );
        await _personService.AddPerson(
          new PersonAddRequest(){PersonName="John",Address="4513 GetOffMyLawn St",Email="John@hotmail.com"}
        );
        // get the list of persons
        list_of_persons = await _personService.GetAllPersons();
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
public async Task GetFilteredPersonsTest(FilteredPersonsOptions option) {
  switch(option) {
    case FilteredPersonsOptions.SomePersons:
      List<PersonResponse> list_response_persons_added = await AddToPersons();
      List<PersonResponse> actual_list = await _personService.GetFilteredPersons(nameof(Person.PersonName), "Tresten");
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
public async Task GetSortedPersonsTest(SortedPersonsEnum options) {

  // variable to be used in switch branch
  List<PersonResponse> expected;
  List<PersonResponse> actual;

  switch(options) {
    case SortedPersonsEnum.AscendingPersonName:
      expected = await AddToPersons();
      expected = expected.OrderBy(p => p.PersonName).ToList();
      actual = _personService.GetSortedPersons(expected,nameof(Person.PersonName),SortOrderEnum.Ascending);
      Assert.Equal(expected, actual);
      break;

    case SortedPersonsEnum.DescendingPersonName:
      expected = await AddToPersons();
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
public async Task UpdatePersonTest(UpdatePersonEnum option) {
  switch(option) {

    case UpdatePersonEnum.NullPersonUpdateRequest:
      PersonUpdateRequest? nullPersonUpdateRequest = null;
      await Assert.ThrowsAsync<ArgumentNullException>(async () => await _personService.UpdatePerson(nullPersonUpdateRequest));
      break;

    case UpdatePersonEnum.ValidPersonUpdateRequest:
      // add to the list of persons
      List<PersonResponse> personsAdded = await AddToPersons();
      // grab the first person in the list
      PersonResponse personToUpdate = personsAdded[0];
      // convert the personResponse to a PersonUpdateRequest
      PersonUpdateRequest personUpdateRequest = personToUpdate.ToPersonUpdateRequest();
      // change something in the personName
      personUpdateRequest.PersonName = "NotMyName Johnson";
      // Attempt to update the person
      PersonResponse actual = await _personService.UpdatePerson(personUpdateRequest);

      // assert the person name is set to what we changed
      Assert.True(actual.PersonName == "NotMyName Johnson");
      break;
  }
}

public enum DeletePersonEnum {
  NullPerson,
  ValidPerson,
  InvalidPerson
}
[Theory]
[InlineData(DeletePersonEnum.NullPerson)]
[InlineData(DeletePersonEnum.ValidPerson)]
[InlineData(DeletePersonEnum.InvalidPerson)]
public async Task DeletePersonTest(DeletePersonEnum option) {
  List<PersonResponse> personsAdded;
  PersonResponse? personResponse;
  bool deleted;
  switch(option) {
    case DeletePersonEnum.NullPerson:
      // add to the list of person
      personsAdded = await AddToPersons();
      // passing null to delete person throws an exception
      await Assert.ThrowsAsync<ArgumentNullException>(async () => {
        await _personService.DeletePerson(null);
      });
      break;

    case DeletePersonEnum.ValidPerson:
      // add to the list of person
      personsAdded = await AddToPersons();
      // grab the first person in the list
      personResponse = personsAdded[0];
      // delete the person
      deleted = await _personService.DeletePerson(personResponse.PersonId);
      // make sure the person was deleted from the list
      Assert.DoesNotContain(personResponse, await _personService.GetAllPersons());
      // make sure the response was true
      Assert.True(deleted);
      break;

    case DeletePersonEnum.InvalidPerson:
      // add to the list of person
      personsAdded = await AddToPersons();
      // Attempt to delete a person that doesn't exist
      deleted = await _personService.DeletePerson(Guid.NewGuid());
      // make sure the response was true
      Assert.False(deleted);
      break;
  }
}


/// <summary>
/// Returns a List<PersonResponse> of all of the persons that have been added
/// </summary>
/// <returns></returns>
private async Task<List<PersonResponse>> AddToPersons(){
  List<PersonResponse> list_persons_added = new();

  list_persons_added.Add(await _personService.AddPerson(
    new PersonAddRequest(){
      PersonName="Tresten",Address="1120 Rex Dr",Email="trestenp@gmail.com"
    }
  ));

  list_persons_added.Add(await _personService.AddPerson(
    new PersonAddRequest(){
      PersonName="Yahan",Address="322 mulburry dr",Email="yahan@yahoo.com"
    }
  ));

  list_persons_added.Add(await _personService.AddPerson(
    new PersonAddRequest(){
      PersonName="Dave",Address="1243 Qwerty St",Email="DaveB@yahoo.com"
    }
  ));

  return list_persons_added;
}

}