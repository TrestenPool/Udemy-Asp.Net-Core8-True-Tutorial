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

      // the value is a string
      if(value is string propertyName) {

        // grabs the other property 
        PropertyInfo? otherProperty = validationContext.ObjectType.GetProperty(OtherPropertyName);

        var validationInstance = validationContext.ObjectInstance;

      }

    }


  }
  
}