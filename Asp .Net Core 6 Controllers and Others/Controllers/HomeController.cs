using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Asp_.Net_Core_6_Controllers_and_Others.Controllers
{
   

    //suffix based controller
    [Route("home")]
    [Route("")]
    public class HomeController
    {
        [Route("index")]
        [Route("")]
        public string Index()
        {
            return "Hello World From Home Controller";
        }
        [Route("about")]
        [Route("extrapage/about")]
        public string About()
        {
            return "Hello World About From Controller";
        }
    }
}
