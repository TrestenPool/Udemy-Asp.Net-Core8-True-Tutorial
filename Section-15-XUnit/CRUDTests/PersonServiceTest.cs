using Entities;
using ServiceContracts;
using Services;

namespace CRUDtests;

public class PersonServiceTest {
  // property
  private readonly IPersonService _personService;

  // constructor
  public PersonServiceTest() {
    _personService = new PersonService();
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




}