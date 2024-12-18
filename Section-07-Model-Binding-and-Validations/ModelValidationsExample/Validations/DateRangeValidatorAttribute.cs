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
      // get the value in a datetime variable
      DateTime to_date = Convert.ToDateTime(value);

      // gets the property
      PropertyInfo? otherProperty = validationContext.ObjectType.GetProperty(OtherPropertyName);

      // the other property is valid
      if(otherProperty != null) {
        
        // get the value of the from_date variable
        DateTime from_date = Convert.ToDateTime(
          otherProperty.GetValue(validationContext.ObjectInstance)
        );

        // from date is after the to date
        if(from_date > to_date) {
          return new ValidationResult($"From Date {from_date} should be before To Date {to_date}", new string[]{OtherPropertyName} );;
        }
        else {
          return ValidationResult.Success;
        }
      }
    }
    return null;
  }
  
}