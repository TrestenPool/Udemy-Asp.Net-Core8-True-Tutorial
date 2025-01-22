using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace HttpRequestExample.Services;

public class MyService{
  // fields
  private readonly IHttpClientFactory _httpClientFactory;

  // constructor
  public MyService(IHttpClientFactory httpClientFactory) {
    _httpClientFactory = httpClientFactory;
  }

  public async Task Method() {

    // make the http request using the factory to create a client for us
    using(HttpClient client = _httpClientFactory.CreateClient() ) {

      // send the response
      HttpResponseMessage message = await client.SendAsync(
        new HttpRequestMessage(
          HttpMethod.Get, 
          "https://dog.ceo/api/breeds/list/all"
        )
      );

      // read the response
      var content = await message.Content.ReadAsStringAsync();

      

      Console.WriteLine(content);
    }

  }

}