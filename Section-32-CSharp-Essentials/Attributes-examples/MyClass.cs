using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace AttributesDemo
{

    public class MyClass {

      [AllowedValues("Hello", "You were supposed to say hello")]
      public string MyProperty {get;} = String.Empty;

      public MyClass(string myProperty){
        MyProperty = myProperty;
      }

    }
}