using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ModelValidationsExample.Models;

namespace ModelValidationsExample.CustomModelBinders;

public class PersonModelBinder : IModelBinder
{

  public Task BindModelAsync(ModelBindingContext bindingContext){
    // the person we will be binding to
    Person person = new Person();    

    // First name
    if( bindingContext.ValueProvider.GetValue("FirstName").Length > 0 ) {
      var firstName = bindingContext.ValueProvider.GetValue("FirstName").First();
      person.Name = firstName;
    }

    // Last name
    if( bindingContext.ValueProvider.GetValue("LastName").Length > 0 ) {
      var lastName = bindingContext.ValueProvider.GetValue("LastName").First();
      person.Name += $"{(string.IsNullOrEmpty(person.Name) ? "":" ")}{lastName}";
    }

    // set the bindingcontext result to our person
    bindingContext.Result = ModelBindingResult.Success(person);
    return Task.CompletedTask;
  }

}