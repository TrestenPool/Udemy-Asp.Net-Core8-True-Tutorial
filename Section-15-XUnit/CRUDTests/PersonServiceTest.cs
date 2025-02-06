using Entities;
using ServiceContracts;
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


}