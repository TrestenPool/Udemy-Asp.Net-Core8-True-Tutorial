using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_App.Validations;

public class MinimumYearAttribute : ValidationAttribute{
  // properties
  public string MinDate;

  // constructor
  public MinimumYearAttribute(string dateTime){
    MinDate = dateTime;
  }

  // isValid method
  protected override ValidationResult? IsValid(
    object? value, 
    ValidationContext validationContext){

    // convert the datetime
    DateTime minimumDateTime = Convert.ToDateTime(MinDate);

    // value is not null
    if (value != null) {

      // convert the value to a datetime object
      if(DateTime.TryParse(value.ToString(), out DateTime dateTimeValue)) {
        if(minimumDateTime > dateTimeValue) {
          return new ValidationResult($"{dateTimeValue} must be greater than {minimumDateTime}");
        }
        else {
          return ValidationResult.Success;
        }
      }

    }

    return ValidationResult.Success;
  }

}