using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WeatherApp.Model;

namespace WeatherApp.Controllers;

[Route("/")]
public class HomeController : Controller{

  [Route("")]
  [Route("home")]
  public IActionResult Index(){
    ViewData["citiesInfo"] = CityWeather.LoadData();
    return View("Index");
  }

  [Route("weather/{cityCode}")]
  public IActionResult Detail([FromRoute]string cityCode) {
    List<CityWeather> cityWeathers = CityWeather.LoadData();
    var result = cityWeathers.Where(x=>x.CityUniqueCode == cityCode).FirstOrDefault();

    return View("detailView", result);
  }



}