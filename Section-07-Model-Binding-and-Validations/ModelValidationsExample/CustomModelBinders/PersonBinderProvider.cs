using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using ModelValidationsExample.Models;

namespace ModelValidationsExample.CustomModelBinders;

public class PersonBinderProvider : IModelBinderProvider
{

  public IModelBinder? GetBinder(ModelBinderProviderContext context){
    // if the type is of type person, use PersonModelBinder
    if(context.Metadata.ModelType == typeof(Person)) {
      return new BinderTypeModelBinder(typeof(PersonModelBinder));
    }

    // otherwise no default modelbinder
    return null;
  }

}