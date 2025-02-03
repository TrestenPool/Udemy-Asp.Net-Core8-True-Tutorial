using Entities;

namespace ServiceContracts;

public class PersonResponse{

}

public static class PersonResponseExtensions {

  public static PersonResponse? ToPersonResponse(this Person person){

    return new PersonResponse(){
    };

  }
  
}
