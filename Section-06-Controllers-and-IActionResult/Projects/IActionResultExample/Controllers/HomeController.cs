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
          if(!Request.Query.ContainsKey("bookid")) {
            return BadRequest("bookid is not supplied");
          }
          else {
            var bookid = Convert.ToInt32(Request.Query["bookid"]);
            if(bookid > 100 || bookid < 0) {
              return NotFound($"bookid of {bookid} was not found");
            }
            return Ok($"welcome to the library with bookid {bookid}");
          }
        }

        [Route("notbooks")]
        public IActionResult BookStore() {
          return new RedirectToActionResult("Books", "StoreController", new {});
          // return RedirectPermanent("/store/books");
          // return RedirectToPage("/store/books");
        }

    }

}