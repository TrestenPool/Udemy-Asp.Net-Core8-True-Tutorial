using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ModelValidationsExample.Validations;

public class MinimumYearValidatorAttribute : ValidationAttribute{

  // properties
  public int MinimumYear { get; set; } = 2000;

  // constructor
  public MinimumYearValidatorAttribute(int minimumYear)
  {
    MinimumYear = minimumYear;
  }

  /*
    method that determines if the model property fails or passes
  */
  protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
  {
    // add the minimimum year to the error string
    ErrorMessage = String.Format(ErrorMessage ?? "Error message {0}", MinimumYear);

    // valid is not null
    if(value != null) {

      // the value passed is a dateonly object
      if(value is DateOnly dateOnly) {

        // the year is not in the range
        if(dateOnly.Year < MinimumYear) {
          return new ValidationResult(ErrorMessage);
        }

        // the year is in the range
        else {
          return ValidationResult.Success;
        }

      }
    }

    return null;
  }

  // public override ValidationResult? IsValid(object? value, ValidationContext validationContext){
  //   return null;
  // }

}