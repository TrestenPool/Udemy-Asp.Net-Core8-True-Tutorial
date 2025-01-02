using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComponentsExample.Models;
using Microsoft.AspNetCore.Mvc;

namespace ComponentsExample.ViewComponents;

[ViewComponent]
public class GridViewComponent : ViewComponent{

  // required, gets invoked when you call the viewcomponent from any of the views
  public async Task<IViewComponentResult> InvokeAsync(PersonGridModel? grid) {

    if(grid == null) {
      PersonGridModel personGridModel= new PersonGridModel(){
        GridTitle = "Persons List",
        Persons = new List<Person>(){
          new Person(){Name="Tresten", JobTitle="Software engineer"},
          new Person(){Name="Dave", JobTitle="Plubmer"},
          new Person(){Name="Erin", JobTitle="Waitress"}
        }
      };
      return View(personGridModel); // invoke a partial view Views/Shared/Components/Grid/Default.cshtml
    }
    else {
      return View(grid);
    }

  }

  // synchronous method instead of async InvokeAsync()
  // IViewComponentResult Invoke(){

  // }

}