using Microsoft.AspNetCore.Mvc;

namespace Asp_.Net_Core_6_Controllers_and_Others.Controllers
{
    //attribute based controller
    [Controller]
    
    public class Contact
    {
        
        public string Index()
        {
            return "Contact Us";
        }
    }
}
