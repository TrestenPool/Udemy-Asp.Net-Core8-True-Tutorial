using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Logging;

// models
using Sample1.Models;


/*
  How to use this controller class in the Program.cs
  1. Add to services to register for depencency injection container
  2. enable routing for the methods
*/
namespace Sample1.Controllers
{
    [Controller]
    public class HomeController : Controller {

      [Route("home")]
      [Route("/")]
      public ContentResult Index() {
        return Content("<h1>Home route</h1>", "text/html");
      }

      [Route("person")]
      public JsonResult Person() {
        // create the person object
        Person person = new Person(){
          Id = Guid.NewGuid(),
          Firstname = "Tresten", 
          Lastname = "Pool",
          Age = 25
        };

        return Json(person);
      }

      [Route("file-download")]
      public IActionResult FileDownload() {
        byte[] bytes = System.IO.File.ReadAllBytes
          (@"C:\Users\trest\Github\Udemy-Asp.Net-Core8-True-Tutorial\Section-06-Controllers-and-IActionResult\Projects\Sample1\wwwroot");
        return new FileContentResult(bytes,"application/text");
      }


    }
}