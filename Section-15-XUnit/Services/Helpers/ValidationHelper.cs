using System.ComponentModel.DataAnnotations;

namespace Services;

public static class ValidationHelper{
  internal static void ModelValidation(object o){
    List<ValidationResult> validationResults = new ();
    bool isValid = Validator.TryValidateObject(o,new ValidationContext(o),validationResults,true);

    // throw exception if there was an error
    if(!isValid) {
      throw new ArgumentException(validationResults.FirstOrDefault()?.ErrorMessage);
    }
  }
}
