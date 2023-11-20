namespace Asp_.Net_Core_6_Controllers_and_Others.CustomConstraints
{
    public class CustomConstraint1 : IRouteConstraint
    {
        //route is the actual route 
        //route key is the paramtere on which we are apply this constriant 
        //values is the dict in which we pass the key to get value
        public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (values[routeKey].ToString() == "hashir")
            {
                return true;
            }
            return false;
        }
    }
}
