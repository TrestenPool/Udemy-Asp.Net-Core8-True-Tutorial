using HttpRequestExample.Services;
using Microsoft.AspNetCore.Mvc;

namespace HttpRequestExample.Controllers;

[Route("/")]
public class HomeController : Controller {
  private readonly IConfiguration _config;
  private readonly FinnhubService _finnhubService;

  public HomeController(IConfiguration config, FinnhubService finnhubService) { 
    _config = config;
    _finnhubService = finnhubService;
  }

  [Route("")]
  public async Task<ActionResult> Index() {
    // gets the dictionary from the service
    var data = await _finnhubService.GetMarketStatus();

    // pass the dictionary to the view
    ViewData["dictionary"] = data;

    // render the view
    return View();
  }



} 