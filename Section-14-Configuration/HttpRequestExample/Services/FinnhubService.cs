using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using HttpRequestExample.ServiceContracts;

namespace HttpRequestExample.Services;

public class FinnhubService : IFinnhubService{
  // fields
  private readonly IHttpClientFactory _httpClientFactory;
  private readonly IConfiguration _configuration;

  // constructor
  public FinnhubService(IHttpClientFactory httpClientFactory, IConfiguration configuration) {
    _httpClientFactory = httpClientFactory;
    _configuration = configuration;
  }

  public async Task<Dictionary<string,object>?> GetMarketStatus() {

    // get the api token from settings
    string apiToken = _configuration["ApiKey"] ?? "";

    // make the http request using the factory to create a client for us
    using(HttpClient client = _httpClientFactory.CreateClient() ) {

      // send the response
      HttpResponseMessage message = await client.SendAsync(
        new HttpRequestMessage(
          HttpMethod.Get, 
          $"https://finnhub.io/api/v1/stock/market-status?exchange=US&token={apiToken}"
        )
      );

      // read the response
      string content = await message.Content.ReadAsStringAsync();

      // store the json content in a dictionary
      var dictionary = JsonSerializer.Deserialize<Dictionary<string,object>>(content);

      if(dictionary == null) {
        throw new InvalidOperationException($"No response from finnhubserver");
      }
      if(dictionary.ContainsKey("Error")) {
        throw new InvalidOperationException($"There was an error in the response from finnhub server\nERROR: {dictionary["Error"]}");
      }

      return dictionary;
    }

  }

}