using System.ComponentModel.DataAnnotations;

namespace Services;

public static class ValidationHelper{
  internal static void ModelValidation(object o){
    // where the validation errors get stored
    List<ValidationResult> validationResults = new ();

    // checks if the model is valid
    bool isValid = Validator.TryValidateObject(o,new ValidationContext(o),validationResults,true);

    // throw exception if there was an error
    if(!isValid) {
      // throw error with what is stored in the validationResults list
      throw new ArgumentException(validationResults.FirstOrDefault()?.ErrorMessage);
    }
  }
}
