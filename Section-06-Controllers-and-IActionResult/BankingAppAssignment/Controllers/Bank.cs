using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BankingAppAssignment.Controllers;

[ApiController]
[Route("/")]
public class Bank : ControllerBase{
    public static int totalAmount = 0;
    
    [Route("/")]
    [HttpGet]
    public IActionResult Index() {
      return Ok($"Total amount = {totalAmount}");
    }

    [Route("/deposit/{amount:int}")]
    [HttpGet]
    public IActionResult Deposit() {

      // amount not supplied
      if(!HttpContext.Request.RouteValues.ContainsKey("amount")) {
        return BadRequest("Must supply amount query parameter ...");;
      }

      // get the amount
      var amount = Convert.ToInt32( HttpContext.Request.RouteValues["amount"] );

      // save old total, add to total
      var oldtotal = totalAmount;
      totalAmount += amount;

      return Ok($"Added {amount} to {oldtotal} now = {totalAmount}");
    }
    
    [Route("/witdraw/{amount:int}")]
    [HttpGet]
    public IActionResult Withdraw() {

      // amount not supplied
      if(!HttpContext.Request.RouteValues.ContainsKey("amount")) {
        return BadRequest("Must supply amount query parameter ...");;
      }

      // get the amount
      var amount = Convert.ToInt32( HttpContext.Request.RouteValues["amount"] );

      // save old total, add to total
      var oldtotal = totalAmount;
      totalAmount -= amount;

      return Ok($"Subtracted {amount} from {oldtotal} now = {totalAmount}");
    }


}