using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ViewsExample.Models;

namespace ViewsExample.Controllers;

[Route("/")]
public class HomeController : Controller{

  // index page
  [Route("")]
  [Route("home")]
  public IActionResult Index() {
    /*
      Examples:
      * If, string interpolation, switch
    */
    // return View(); 
    
    /*
      Examples:
      * foreach, for, local functions, methods
      * local functions do not have the @functions word, methods do
    */
    return View("Index2");
  }

  /*
    ViewData
    - how data is sent from controller to view
    - a dictionary obj that is sent from controller to the view
    - it is automatically created for a request, and deleted before sending response back to the client
  */
  [Route("sendingData")]
  public IActionResult sendingData() {

    // list of people
    List<Person> people = new List<Person>(){
      new(){Name="Tresten",Dob=new DateTime(1999,1,29)},
      new(){Name="John",Dob=new DateTime(1967,12,12)},
      new(){Name="Dave"},
      new(){Name="Rex",Dob=new DateTime(1973,7,22)}
    };

    ViewData["people"] = people;
    ViewData["pageTitle"] = "Asp.Net Core Demo app";

    return View();
  }

  
  [Route("stronglyTypedView")]
  public IActionResult stronglyTypedView() {
    List<Person> people = new List<Person>(){
      new(){Name="Tresten",Dob=new DateTime(1999,1,29)},
      new(){Name="John",Dob=new DateTime(1967,12,12)},
      new(){Name="Dave"},
      new(){Name="Rex",Dob=new DateTime(1973,7,22)}
    };
    return View("Index3", people);
  }

  [Route("person/{idx}")]
  public IActionResult detailView([FromRoute]int idx) {
    List<Person> people = new List<Person>(){
      new(){Name="Tresten",Dob=new DateTime(1999,1,29)},
      new(){Name="John",Dob=new DateTime(1967,12,12)},
      new(){Name="Dave"},
      new(){Name="Rex",Dob=new DateTime(1973,7,22)}
    };

    // idx out of range
    if(idx < 0 || idx >= people.Count) {
      return View("Index4", null);
    } 

    // render the page with the person
    return View("Index4", people[idx]);
  }


  [Route("person-with-product")]
  public IActionResult PersonWithProduct() {
    Person person = new Person(){Name="John wayne", Dob = new DateTime(1999,1,29), PersonGender=Gender.Male};
    Product product = new Product(){Id=1, Name="Air conditioner"};

    PersonAndProductWrapper personAndProductWrapper = new PersonAndProductWrapper(){
      PersonData = person,
      ProductData = product
    };

    // Setting ViewData, sending as ViewData too
    ViewData["Title"] = "This is my title";
    ViewData["person"] = person;
    ViewData["product"] = product;

    return View("Index5", personAndProductWrapper);
  }

}