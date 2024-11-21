using RouteParameters.Custom_Constraints;

namespace RouteParameters {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            // add the custom constraint
            builder.Services.AddRouting(options => {
                options.ConstraintMap.Add("months", typeof(MonthsCustomConstraint));
            });

            var app = builder.Build();

            // enable routing
            app.UseRouting();

            /*
                no longer have to nest inside of app.UseEndpoints.
                Instead it is recommended to define at the top level like the example below this comment

                app.UseEndpoints(endpoints => {
                    endpoints.MapGet("files/{filename}.{extension}", async (context) => {
                    await context.Response.WriteAsync("In files");
                    });
                });
            */

            #pragma warning disable
            app.UseEndpoints(endpoints => {
                // files
                endpoints.Map("/files/{filename}.{extension}", async (context) => {
                    string filename = context.Request.RouteValues["filename"].ToString();
                    string extension = context.Request.RouteValues["extension"].ToString();
                    await context.Response.WriteAsync($"In files {filename} {extension}\n");
                });

                // employees
                endpoints.Map("employee/profile/{employeename=TrestenPool}", async (context) => {
                    string empName = context.Request.RouteValues["employeename"].ToString();
                    await context.Response.WriteAsync($"In employee profile {empName}\n");
                });

                // products
                endpoints.MapGet("products/details/{id:int?}", async context => {
                    string? id = null;
                    if(context.Request.RouteValues.ContainsKey("id")) {
                        id = context.Request.RouteValues["id"].ToString();
                    }
                    await context.Response.WriteAsync($"In products {id ?? "no id provided"}\n");
                });

                // guid
                endpoints.MapGet("cities/{cityid:guid}", async context => {
                    string? cityid = null;
                    if(context.Request.RouteValues.ContainsKey("cityid")) {
                        cityid = context.Request.RouteValues["cityid"].ToString();
                    }
                    await context.Response.WriteAsync($"city id = {cityid}");
                });

                // length
                endpoints.MapGet("company/{abrev:length(2,3):alpha}", async context => {
                    string? abrev = null;
                    if(context.Request.RouteValues.ContainsKey("abrev")) {
                        abrev = context.Request.RouteValues["abrev"].ToString();
                    }
                    await context.Response.WriteAsync($"city id = {abrev}");
                });

                // regex, accept an ip address
                endpoints.MapGet("ip/{ipaddr:regex(^\\d{{1,3}}.\\d{{1,3}}.\\d{{1,3}}.\\d{{1,3}}$)}", async context => {
                    string? ipaddr = null;
                    if(context.Request.RouteValues.ContainsKey("ipaddr")) {
                        ipaddr = context.Request.RouteValues["ipaddr"].ToString();
                    }
                    await context.Response.WriteAsync($"ip address = {ipaddr}");
                });

                // custom constraint
                endpoints.MapGet("report/{year:int:min(1900)}/{month:months}", async context => {
                    int? year = Convert.ToInt32(context.Request.RouteValues["year"]);
                    string? month = Convert.ToString(context.Request.RouteValues["month"]);

                    await context.Response.WriteAsync($"year = {year}\nmonth = {month}");
                });

                
            });
            #pragma warning enable

            // defining routes
            //app.MapGet("files/{filename}.{extension}", async (context) => {
            //    await context.Response.WriteAsync("In files");
            //});

            // this middleware will catch all other routes that are not listed above
            app.Run(async (context) => {
                await context.Response.WriteAsync($"Request received at {context.Request.Path}");
            });

            app.Run();
        }
    }
}
