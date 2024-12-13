using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sample1.Models;

namespace Sample1.Controllers
{
    [ApiController]
    [Route("/")]
    public class HomeController : ControllerBase
    {
        
        [Route("/")]
        public IActionResult Index() {
          return Ok("Home route");
        }

        [HttpPost]
        [Route("/book")]
        public IActionResult FetchBook([FromForm]Book book) {
          return Ok(book.ToString());
        }
    }
}