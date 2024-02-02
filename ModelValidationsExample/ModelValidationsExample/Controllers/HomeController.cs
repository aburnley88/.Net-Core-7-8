using Microsoft.AspNetCore.Mvc;
using ModelValidationsExample.Models;

namespace ModelValidationsExample.Controllers
{
    public class HomeController : Controller
    {
        [Route("Register")]
        public IActionResult Index(Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(string.Join("\n", ModelState.Values.SelectMany(value=> value.Errors)
                    .Select(error => error.ErrorMessage)));
            }
            return Content($"{person}");
        }
    }

}
