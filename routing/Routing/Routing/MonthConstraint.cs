using System.Text.RegularExpressions;

namespace Routing
{
    public class MonthConstraint : IRouteConstraint
    {
        public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if(!values.ContainsKey(routeKey)) return false;

            Regex regex = new Regex("^(apr|jul|oct|jan)$");
            string? monthVal = Convert.ToString(values[routeKey]);
            return regex.IsMatch(monthVal) ? true : false;
        }
    }
}
