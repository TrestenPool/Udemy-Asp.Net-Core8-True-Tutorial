using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ModelValidationsExample.CustomModelBinders;
using ModelValidationsExample.Models;

namespace ModelValidationsExample.Controllers;
[Route("/")]
public class HomeController : ControllerBase {

  [Route("hey")]
  public IActionResult Index() {
    return Ok("Hello world");
  }

  [Route(template:"person")]
  public IActionResult FetchPerson(
    // no longer needed because defined ModelBinderProvider
    // [ModelBinder(BinderType=typeof(PersonModelBinder))]
    Person person,
    
    // grab the 
    [FromHeader(Name = "User-Agent")]
    string userAgent) {

    // model validation has some errors
    if(!ModelState.IsValid) {
      List<string> errorList = ModelState.Values.SelectMany(value => value.Errors).Select(error => error.ErrorMessage).ToList();
      string errors = string.Join("\n", errorList);
      return BadRequest(errors);
    }

    return Content($"{person.Name}, {userAgent}");

    // return the successfull json result
    // return new JsonResult(person);
  }

  [Route("{num}/{letter?}")]
  public IActionResult Something(int? num, string? letter) {
    return Ok(letter+num);
  }
}