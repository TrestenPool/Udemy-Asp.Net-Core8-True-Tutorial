
using System.Text.RegularExpressions;

namespace RouteParameters.Custom_Constraints {
    public class MonthsCustomConstraint : IRouteConstraint {
        // interface method we have to implement
        public bool Match(
                // our context
                HttpContext? httpContext, 
                // the route we are currently checking if we match
                IRouter? route, 
                // not sure
                string routeKey, 
                // route value dictionary
                RouteValueDictionary values, 
                // route direction: for an incoming request or for the generation of a url
                RouteDirection routeDirection) {

            // checks if the 
            if(!values.ContainsKey(routeKey)) {
                return false;
            }

            // check if our month value matches our regex
            Regex regex = new Regex($"^(apr|jul|oct|jan)$");
            string? monthValue = Convert.ToString(values[routeKey]);

            if(regex.IsMatch(monthValue ?? "")) {
                return true;
            }
            else {
                return false;
            }

        }
    }
}
