using Microsoft.AspNetCore.Components;

using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
namespace Asp_.Net_Core_6_Controllers_and_Others.Controllers
{
    [Route("[controller]/[action]")]
    public class AboutusController
    {
        public string Index()
        {
            return "About us";
        }
        

        public string about1()
        {
            return "About1";
        }

        [Route("~/aboutus/newabout")]
        public string about2()
        {
            return "About2";
        }
    }
}
