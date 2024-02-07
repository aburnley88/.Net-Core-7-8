using Microsoft.AspNetCore.Mvc;
using WeaatherApp.Models;

namespace WeaatherApp.Controllers
{
    public class HomeController : Controller
    {
        private IEnumerable<CityWeather?> locations = new List<CityWeather>
        {
            new CityWeather() { CityCode = "LDN", CityName = "London", DateAndTime = DateTime.Parse("2030-01-01 08:00"), TempFahrenheit = 33 },
            new CityWeather() { CityCode = "NYC", CityName = "New York City", DateAndTime = DateTime.Parse("2030-01-01 03:00"), TempFahrenheit = 60 },
            new CityWeather() { CityCode = "PAR", CityName = "Paris", DateAndTime = DateTime.Parse("2030-01-01 09:00"), TempFahrenheit = 82 }
        };

        [Route("/")]
        public IActionResult Index( )
        {
        

           ViewBag.title = "Welcome";
           return View("index", locations);
        }
        [HttpGet]
        [Route("/weather/{cityCode:alpha}")]
        public IActionResult Weather(string cityCode)
        {
            var validCodes = new List<string> { "LDN", "NYC", "PAR" };
            ViewBag.Title = "Weather";

            return validCodes.Contains(cityCode) ? View("Weather", locations.Where(x => x.CityCode == cityCode).FirstOrDefault()) :
                Error(cityCode);
        }
        public IActionResult Privacy()
        {
            return View();
        }

   
        public IActionResult Error(string cityCode)
        {
            ViewBag.Title = "Error";
            return View("Error", new CityWeather { CityCode = cityCode });
        }
    }
}
