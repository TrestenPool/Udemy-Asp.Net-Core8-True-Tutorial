using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ModelValidationsExample.Validations;

public class DateRangeValidatorAttribute : ValidationAttribute{

  /****** Properties *********/
  // the other date we will be comparing against
  public string OtherPropertyName { get; set; }


  /****** Constructor *********/
  public DateRangeValidatorAttribute(string otherPropertyName) {
    OtherPropertyName = otherPropertyName;
  }
  
  /** isvalid method **/
  protected override ValidationResult? IsValid(object? value, ValidationContext validationContext){

    if(value != null) {
      // try to parse string to datetime variable
      if( DateTime.TryParse(value.ToString(), out DateTime mydate) ) {
        Console.WriteLine("converted to mydate");
      }
      else {
        Console.WriteLine("Unable to convert to datetime");
      }

    }

    // return successful other wise
    return ValidationResult.Success;
  }
  
}