using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherAssignment.Models;

public class CityWeather{
  public string? CityUniqueCode {get; set;}
  public string? CityName {get; set;}
  public DateTime? DateAndTime {get; set;}
  public int? TemperatureFahrenheit {get; set;}

  public static List<CityWeather> LoadData() {
    return new List<CityWeather>{
      new(){
        CityUniqueCode="LDN",
        CityName="London",
        DateAndTime=new DateTime(1999, 1, 29),
        TemperatureFahrenheit=70
      },
      new(){
        CityUniqueCode="NYC",
        CityName="New York",
        DateAndTime=new DateTime(2000, 3, 12),
        TemperatureFahrenheit=50
      },
      new(){
        CityUniqueCode="PAR",
        CityName="Paris",
        DateAndTime=new DateTime(2006, 5, 10),
        TemperatureFahrenheit=70
      }
    };
  }
}
