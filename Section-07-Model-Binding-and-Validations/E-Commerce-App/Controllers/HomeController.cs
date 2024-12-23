using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commerce_App.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_App.Controllers;
[Route("/")]
public class HomeController : Controller{

  [Route("")]
  public IActionResult Index() {
    return Ok("Home index page");
  }

  [Route("order")]
  public IActionResult Orders(
    [FromBody]Order order) {

    // return the errors in a bad request response
    if(!ModelState.IsValid) {
      var errors = ModelState.Values
        .SelectMany(v => v.Errors)
        .Select(e => e.ErrorMessage);

      return BadRequest(errors);
    }

    // generate random number
    Random random = new Random();
    int rand = random.Next(1,9999);

    // set the order no on the order object
    order.OrderNo = rand;

    // return Ok(rand);
    return new JsonResult(order);
  }

}