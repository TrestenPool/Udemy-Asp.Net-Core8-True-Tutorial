var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// enable routing
app.UseRouting();

// create our dictionary
var countries = new Dictionary<int,string>(){
  { 1, "United States" },
  { 2, "Canada" },
  { 3, "United Kingdom" },
  { 4, "India" },
  { 5, "Japan" }
};

app.UseEndpoints(endpoints => {

  _ = endpoints.MapGet("/countries", async (HttpContext context) =>
  {
    string? mystring = string.Empty;
    foreach (var country in countries)
    {
      mystring += country.Value + "\n";
    }
    await context.Response.WriteAsync(mystring);
  });

  _ = app.MapGet("/countries/{id:int:required}", async (HttpContext context) => {

    // get the country id from route params
    var countryId = Convert.ToInt32( context.Request.RouteValues["id"] );

    // country id is > 100
    if(countryId > 100) {
      context.Response.StatusCode = 400;
      await context.Response.WriteAsync("The country id should be between 1 and 100");
      return;
    }

    // return the country since it exists
    if(countries.ContainsKey(countryId)){
      await context.Response.WriteAsync(countries[countryId]);
      return;
    }
    // the country does not exist
    else {
      context.Response.StatusCode = 404;
      await context.Response.WriteAsync($"The id {countryId} was not found");
      return;
    }

  });

});



app.Run(async (context) => {
  await context.Response.WriteAsync("Final catching middleware");
});

// terminating middleware
app.Run();