using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace HttpRequestExample.Services;

public class MyService{
  // fields
  private readonly IHttpClientFactory _httpClientFactory;
  private readonly IConfiguration _configuration;

  // constructor
  public MyService(IHttpClientFactory httpClientFactory, IConfiguration configuration) {
    _httpClientFactory = httpClientFactory;
    _configuration = configuration;
  }

  public async Task<Dictionary<string,object>?> Method() {

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

      return dictionary;

      // // there was no response from the api
      // if(responseDictionary == null) {
      //   throw new InvalidOperationException("No response from api server");
      // }

      // // The api sent back an error
      // if(responseDictionary.ContainsKey("error")) {
      //   throw new InvalidOperationException(
      //     Convert.ToString(responseDictionary["error"])
      //   );
      // }

      // // return the dictionary to the user
      // return responseDictionary;
    }

  }

}