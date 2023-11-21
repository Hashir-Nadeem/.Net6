using Microsoft.AspNetCore.Mvc;

namespace Asp_.Net_Core_6_Controllers_and_Others.Areas.admin.Controllers
{
    [Area("adminpanel")]
    [Route("{area:exists}/[controller]/[action]")]
    public class adminController : Controller
    {
        public IActionResult Index()
        {
            return Ok("Admin Panel");
        }
    }
}
