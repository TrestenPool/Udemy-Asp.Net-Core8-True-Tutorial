using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ModelValidationsExample.Models;

namespace ModelValidationsExample.Controllers
{
    [Route("/")]
    public class HomeController : ControllerBase {
        
        [Route("/")]
        public IActionResult Index() {
          return Ok("Hello world");
        }

        [Route("/person")]
        public IActionResult FetchPerson([FromBody]Person person) {

          // clears validation state of Price key
          // ModelState.ClearValidationState("Price");
          // clear the whole thing
          // ModelState.Clear();

          // bool result = TryValidateModel(person);

          if(!ModelState.IsValid) {
            /* lengthy iteration way */
            // List<string> errorsList = new List<string>();
            // foreach(var value in ModelState.Values) {
            //   foreach(var error in value.Errors) {
            //     errorsList.Add(error.ErrorMessage);
            //   }
            // }
            // string errors = string.Join("\n", errorsList);
            // return BadRequest(errors);


            /* LINQ way of returning all of the error messages to the user */
            List<string> errorList = ModelState.Values.SelectMany(value => value.Errors).Select(error => error.ErrorMessage).ToList();
            string errors = string.Join("\n", errorList);
            return BadRequest(errors);


            /* easiest way */
            // return BadRequest(ModelState);
          }

          return new JsonResult(person);
        }

    }
}