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
        [Route("books")]
        public IActionResult Index() {
          return Ok("store controller index");
        }
    }
}