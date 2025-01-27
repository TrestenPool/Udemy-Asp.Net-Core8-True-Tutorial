using ServiceContracts;

namespace CRUDTests;

public class CountriesServiceTest{
  private readonly ICountriesService _countriesService;

  public CountriesServiceTest() {
  }

  [Fact]
  public void Test1(){
    // Arrange
    MyMath mm = new MyMath();
    int input1 = 10, input2 = 20;
    int expected = 30;

    // Act
    int actual = mm.Add(input1, input2);

    // Assert
    Assert.Equal(expected, actual);
  }
}