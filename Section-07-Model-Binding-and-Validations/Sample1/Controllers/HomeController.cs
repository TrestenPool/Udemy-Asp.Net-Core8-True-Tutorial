using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Sample1.Controllers
{
    [ApiController]
    [Route("/")]
    public class HomeController : ControllerBase
    {
        [Route("/")]
        public IActionResult Index(int? bookid) {

          if(!bookid.HasValue) {
            return BadRequest("bookid is null");
          }
          
          return Ok($"bookid = {bookid}");
        }
    }
}