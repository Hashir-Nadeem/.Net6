using Microsoft.AspNetCore.Mvc;
using Net6Model_Views.CustomModelBinder;
using Net6Model_Views.Models;

namespace Net6Model_Views.Controllers
{
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //it will get values from query as there is no route parameters define
        public IActionResult getName(string firstname, string lastname)
        {
            return Json(firstname + lastname);
        }

        //it will take value from route if we have query parameters or not does not matter
        [Route("{firstname}/{lastname}")]
        public IActionResult getNameRoute(string firstname,string lastname)
        {
            return Json(firstname + lastname);
        }

        //it will ignore precedence level
        [Route("{firstname}/{lastname}")]
        public IActionResult getNameSpecific([FromQuery]string firstname, string lastname)
        {
            return Json(firstname + lastname);
        }

        //it will take form data first
        [Route("{firstname}/{lastname}")]
        public IActionResult getNameForm(string firstname,string lastname)
        {
            return Json(firstname + lastname);
        }

        //it will take form data first
        [Route("{firstname}/{middlename}/{lastname}")]
        public IActionResult getNameFormSpecific([FromRoute]string firstname,[FromQuery]string middlename ,[FromForm]string lastname)
        {
            return Json(firstname + middlename +lastname);
        }

        //object data will extrct from form route or query according to the precedence without mentioning anything
        public IActionResult getPerson(Person person)
        {
            return Json(person);
        }

        //is we are getting data form form route or query it will bind automatically but if we are getting data in body so we need to define
        //this frombody becuase it will call inputformatter for particular format like for json and text and html they are default
        //and for xml you need to add service in program .cs 
        public IActionResult getPersonJsonXml([FromBody]Person person)
        {
            return Json(person);
        }

        //get data from header and collection binding
        public IActionResult getHeaderData([FromHeader(Name="custom-header")] string custom,Person person)
        {
            return Json(custom);
        }

        //only bind that are mention
        //nameof attribute return the name of the field as string it is used to avoid error that can occur in string literal like
        //spelling mistake or we can add feild that not present or convention mistake
        //type of is used to get the type of the object at runtime 
        public IActionResult bindSpecific([ModelBinder(BinderType =typeof(PersonModelBinder))][Bind(nameof(Person.FirstName),nameof(Person.LastName))]Person person)
        {
            if (ModelState.IsValid)
            {
                return Json(person);

            }
            return Json(ModelState.Values);
        }

    }
}
