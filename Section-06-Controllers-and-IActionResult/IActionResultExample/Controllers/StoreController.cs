using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace IActionResultExample.Controllers
{
    [ApiController]
    [Route("/store")]
    public class StoreController : ControllerBase
    {
        [Route("books/{id=1}")]
        public IActionResult Index() {
          int id = Convert.ToInt32(HttpContext.Request.RouteValues["id"]);
          return Ok($"store id of {id}");
        }
    }
}