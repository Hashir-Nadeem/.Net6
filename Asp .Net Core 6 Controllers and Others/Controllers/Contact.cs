using Microsoft.AspNetCore.Mvc;

namespace Asp_.Net_Core_6_Controllers_and_Others.Controllers
{
    //attribute based controller
    [Controller]
    
    public class Contact:Controller
    {
        public string Index()
        {
            return "Contact Us";
        }

        public IActionResult goToAbout()
        {
            
            //return new RedirectToActionResult("about1", "Aboutus", new { },true);
            return new RedirectToActionResult("about1", "Aboutus", new {});
        }

        public IActionResult goToAbout2()
        {
            //return RedirectToActionPermanent("about1", "Aboutus", new { });
            return RedirectToAction("about1", "Aboutus", new { });

        }
    }
}
