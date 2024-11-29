using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace IActionResultExample.Controllers
{
    [ApiController]
    [Route("/")]
    public class HomeController : ControllerBase
    {
        [Route("/")]
        [Route("home")]
        public IActionResult Index() {
          // bookid not supplied
          if(!Request.Query.ContainsKey("bookid")) {
            return BadRequest("bookid is not supplied");
          }
          // bookid supplied
          else {
            // get the bookid 
            var bookid = Convert.ToInt32(Request.Query["bookid"]);

            // bookid not found
            if(bookid > 100 || bookid < 0) {
              return NotFound($"bookid of {bookid} was not found");
            }

            // bookid found
            return Ok($"welcome to the library with bookid {bookid}");
          }
        }

        [Route("notbooks")]
        public IActionResult BookStore() {

          // temporary redirect
          // return new RedirectToActionResult("Index", "Store", new {}); // 302

          // permanent redirect
          // return new RedirectToActionResult("Index", "Store", new {}, true); // 301
          
          // shorthand with query parameters
          return RedirectToAction("Index", "Store", new {id=234});

          // return LocalRedirect("/store/books");
          // return LocalRedirectPermanent("/store/books");
        }

    }

}