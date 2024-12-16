using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ModelValidationsExample.Validations;

public class DateRangeValidatorAttribute : ValidationAttribute{

  /****** Properties *********/
  public string OtherPropertyName { get; set; }


  /****** Constructor *********/
  public DateRangeValidatorAttribute(string otherPropertyName) {
    OtherPropertyName = otherPropertyName;
  }
  

  /** isvalid method **/
  protected override ValidationResult? IsValid(object? value, ValidationContext validationContext){
    
    // checks if value is null
    if(value != null) {
      // convert the value to datetime
      DateTime to_date = Convert.ToDateTime(value);
      
      // gets the property info called whose name is stored in OtherPropertyName variable
      PropertyInfo? otherProperty = 
        validationContext.ObjectType.GetProperty(OtherPropertyName);

      // get the value of the other property if there is one
      if(otherProperty != null){

        // get the value of otherProperty
        DateTime from_date = Convert.ToDateTime(otherProperty?.GetValue(validationContext.ObjectInstance));

        // the from date is greater than the to date, fail...
        if(from_date > to_date) {
          return new ValidationResult($"The from date {from_date} should be less than the to_date {to_date}",
            new string[] {OtherPropertyName});
        }
      }
    }

    return ValidationResult.Success;
  }
  
}