using Microsoft.Extensions.Primitives;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace HttpSection {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            //app.MapGet("/", Examples.HelloWorld());
            //app.MapGet("/", Examples.QueryStringExample());
            //app.MapGet("/", Examples.RequestHeadersExample());
            //app.MapPost("/", Examples.PostRequestParsingRaw());
            app.MapGet("/", Examples.OperationAssignment());

            app.Run();
        }


        /*
            This is where our examples live
        */
        public static class Examples {
            public static Func<HttpContext, Task> HelloWorld() {
                async Task HandleRequest(HttpContext context) {
                    await context.Response.WriteAsync("Hello from async function");
                }
                return HandleRequest;

            }

            public static Func<HttpContext, Task> QueryStringExample() {
                async Task HandleRequest(HttpContext context) {
                    context.Response.Headers["Content-type"] = "text/html";
                    if (context.Request.Method == "GET") {
                        if (context.Request.Query.ContainsKey("id")) {
                            string id = context?.Request?.Query["id"];
                            await context.Response.WriteAsync($"<p>You passed id = {id}</p>");
                        }
                        else {
                            await context.Response.WriteAsync($"<p>No id supplied</p>");
                        }
                    }
                    else {
                        await context.Response.WriteAsync("<p>Nothing to see here</p>");
                    }

                    var method = context?.Request?.Method;
                    await context.Response.WriteAsync($"\n<p>{method}</p>");
                }

                return HandleRequest;
            }

            public static Func<HttpContext, Task> RequestHeadersExample() {
                async Task HandleRequest(HttpContext context) {
                    if(context.Request.Headers.Count > 0) {
                        await context.Response.WriteAsync($"Number of headers = {context.Request.Headers.Count}");
                        await context.Response.WriteAsync($"\nAccept = {context.Request.Headers["Accept"]}");
                    }
                    else {
                        await context.Response.WriteAsync($"No headers were passed");
                    }
                }
                return HandleRequest;
            }

            public static Func<HttpContext, Task> PostRequestParsingRaw() {
                async Task HandleRequest(HttpContext context) {

                    // create a stream reader
                    System.IO.StreamReader reader = new StreamReader(context.Request.Body);

                    // read the contents
                    string body = await reader.ReadToEndAsync();

                    // parse the request body
                    Dictionary<string, StringValues> queryDict =
                        Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(body);

                    if(queryDict.ContainsKey("firstName")) {
                        string firstName = queryDict["FirstName"][0];
                        await context.Response.WriteAsync($"firstName = {firstName}");
                    }
                    if(queryDict.ContainsKey("lastName")) {
                        string lastName = queryDict["lastName"][0];
                        await context.Response.WriteAsync($"\nlastName = {lastName}");
                    }


                }
                return HandleRequest;
            }

            public static Func<HttpContext, Task> OperationAssignment() {
                async Task HandleRequest(HttpContext context) {
                    // Define a delegate to represent an operation
                    Func<int, int, int> add = (x, y) => x + y;
                    Func<int, int, int> subtract = (x, y) => x - y;
                    Func<int, int, int> multiply = (x, y) => x * y;
                    Func<int, int, int> divide = (x, y) => x / y;

                    Func<int, int, int> operation = add;


                    int firstNumber = 0;
                    int secondNumber = 0;

                    if(context.Request.Query.ContainsKey("firstNumber")) {
                        int.TryParse(context.Request.Query["firstNumber"], out firstNumber);
                    }
                    if(context.Request.Query.ContainsKey("secondNumber")) {
                        int.TryParse(context.Request.Query["secondNumber"], out secondNumber);
                    }
                    if(context.Request.Query.ContainsKey("operation")) {
                        switch(context.Request.Query["operation"]) {
                            case "+":
                                operation = add;
                                break;
                            case "-":
                                operation = subtract;
                                break;
                            case "*":
                                operation = multiply;
                                break;
                            case "/":
                                operation = divide;
                                break;
                        }
                    }

                    // return the operation
                    await context.Response.WriteAsync($"{operation(firstNumber,secondNumber)}");
                }
                return HandleRequest;
            }

        }// end of Examples

    }// end of Program

}// end of namespace
