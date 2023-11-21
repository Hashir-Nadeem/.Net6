using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Asp_.Net_Core_6_Controllers_and_Others.Controllers
{


    //suffix based controller
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/home")]
    [Route("")]

    public class HomeController:Controller
    {
        private readonly IHttpContextAccessor _httpContext;

        public HomeController(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }
        [Route("index")]
        [Route("")]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]

        public ContentResult Index()
        {
            //if we inherit controller base class then
            //return Content("Hello world from home","text/plain");
            return new ContentResult() { Content="Hello world from home",ContentType = "text/plain",StatusCode=200};
        }
        [Route("about")]
        [Route("extrapage/about")]
        public JsonResult About()
        {
            var etag = "123";
            _httpContext.HttpContext.Response.Headers.ETag = etag;
            //return Json("Hello json world");
            return new JsonResult("hello json world");
        }
        [Route("sample")]
        //if file is present is wwwroot folder and must remember to add app.useStaticFile() middleware
        public VirtualFileResult VirtualFile()
        {
            
            return new VirtualFileResult("/sample.pdf", "application/pdf");
        }

        [Route("physicalsample")]
        //if file is present outside wwwroot folder
        public PhysicalFileResult PhysicalFile()
        {
            return new PhysicalFileResult(@"C:\Users\The Laptop Store\OneDrive\Desktop\ASP.NET Core Middleware _ Microsoft Learn.pdf", "application/pdf");
        }

        [Route("bytearray")]
        public FileContentResult FileContent()
        {
            //byte array of files are used to upload or download files
            var byteArray = System.IO.File.ReadAllBytes(@"C:\Users\The Laptop Store\OneDrive\Desktop\ASP.NET Core Middleware _ Microsoft Learn.pdf");
            return new FileContentResult(byteArray,"application/pdf");
        }

        [Route("getName/{name:alpha:isHashir}")]
        public IActionResult getName()
        {
            var name = _httpContext.HttpContext.Request.RouteValues["name"];
            if (name == null)
            {
                return NotFound("Name Not Found");
            }
            return Ok("Name is: "+name);
        }
    }
}
