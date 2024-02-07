using Microsoft.AspNetCore.Mvc;
using ViewsExample.Models;

namespace ViewsExample.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            ViewData["appTitle"] = "Views Example";
            ViewBag.content = "Welcome to the View";
            List<Person> peopleList = new List<Person>()
            {
                new Person {Name ="Tems", DateOfBirth= Convert.ToDateTime("1993-08-17"), Gender= Gender.Female},
                new Person {Name ="Rihab", DateOfBirth= Convert.ToDateTime("1998-11-27"), Gender= Gender.NonBinary},
                new Person {Name ="Antara", DateOfBirth= Convert.ToDateTime("1992-09-07"), Gender= Gender.TransMan}
            };

     
            return View("Index", peopleList);
        }
        [Route("person/details/{name:alpha}")]
        public IActionResult GetPersonByName(Person person)
        {
            if (string.IsNullOrEmpty(person.Name))
            {
                return Content("Name must be provided");
            }

            List<Person> peopleList = new List<Person>()
            {
                new Person {Name ="Tems", DateOfBirth= Convert.ToDateTime("1993-08-17"), Gender= Gender.Female},
                new Person {Name ="Rihab", DateOfBirth= Convert.ToDateTime("1998-11-27"), Gender= Gender.NonBinary},
                new Person {Name ="Antara", DateOfBirth= Convert.ToDateTime("1992-09-07"), Gender= Gender.TransMan}
            };

            var chosenPerson = peopleList.Where(p => p.Name == person.Name).FirstOrDefault();

            return View("Details", chosenPerson);
        }
    }
}
