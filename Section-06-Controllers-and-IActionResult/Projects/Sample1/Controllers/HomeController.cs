using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

/*
  How to use this controller class in the Program.cs
  1. Add to services to register for depencency injection container
  2. enable routing for the methods
*/
namespace Sample1.Controllers
{
    public class HomeController {

      // attribute routing
      [Route("sayhello")]
      public string method1() {
        return "Hello from method1";
      }

      [Route("goodbye")]
      public string method2() {
        return "Goodbye old friend";
      }


    }
}