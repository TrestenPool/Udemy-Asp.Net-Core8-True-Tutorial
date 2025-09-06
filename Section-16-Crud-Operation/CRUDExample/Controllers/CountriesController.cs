using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;

namespace CRUDExample.Controllers;

[Route("[controller]")]
public class CountriesController : Controller
{
  private readonly ICountriesService _countriesService;

  public CountriesController(ICountriesService countriesService)
  {
    _countriesService = countriesService;
  }

  [Route("[action]")]
  [HttpGet]
  public IActionResult UploadFromExcel()
  {
    return View();
  }

  [Route("[action]")]
  [HttpPost]
  public async Task<IActionResult> UploadFromExcel(IFormFile excelFile)
  {
    // make sure the file is not empty or null
    if (excelFile == null || excelFile.Length == 0)
    {
      ViewData["ErrorMessage"] = "Please select a valid .xlsx file";
      return View();
    }

    // make sure the file is of valid extension
    if (!Path.GetExtension(excelFile!.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
    {
      ViewData["ErrorMessage"] = "You provided an extension that is not .xlsx. It must be .xlsx file";
      return View();
    }

    try
    {
      int countriesinserted = await _countriesService.UploadFromExcelFile(excelFile);
      ViewData["SuccessMessage"] = $"You inserted {countriesinserted} countries";
    }
    catch (Exception e)
    {
      Console.WriteLine($"there was an error {e.Message}");
    }

    return View();
  }

}
