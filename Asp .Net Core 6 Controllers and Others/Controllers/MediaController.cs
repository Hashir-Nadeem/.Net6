using Microsoft.AspNetCore.Mvc;

namespace Asp_.Net_Core_6_Controllers_and_Others.Controllers
{
    public class MediaController : Controller
    {
        private readonly IHttpContextAccessor _httpContext;

        public MediaController(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult pdf()
        {
            var filename = _httpContext.HttpContext.Request.RouteValues["filename"];
            if (filename == null)
            {
                return NotFound("Resource Not Found");
            }
            return Ok(filename);
        }
    }
}
