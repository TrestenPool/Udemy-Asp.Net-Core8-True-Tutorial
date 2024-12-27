using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ViewsExample.Models;

public class Person{
  public string? Name {get; set;}
  public DateTime? Dob {get; set;}
  public Gender PersonGender {get; set;}
}

public enum Gender {
  Male, Female
}