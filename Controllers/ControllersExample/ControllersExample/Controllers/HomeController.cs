using ControllersExample.Models;
using Microsoft.AspNetCore.Mvc;

namespace ControllersExample.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("/Content")]
        public ContentResult ContentResult()
        {
            return Content("<h3>Content Rsponse type from '/content'", "text/html");
        }
        [Route("/person")]
        public JsonResult CreatePerson()
        {
            return Json(new Person()
            {
                Id = Guid.NewGuid(),
                FirstName = "Omari",
                LastName = "Burnley",
                Age = 0,
            });
        }
    }
}
