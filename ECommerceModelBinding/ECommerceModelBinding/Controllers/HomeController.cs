using ECommerceModelBinding.Utility;

namespace ECommerceModelBinding.Controllers
{
    public class HomeController : Controller
    {
        [HttpPost]
        [Consumes("application/x-www-form-urlencoded", "multipart/form-data")]
        [Route("/order")]
        public IActionResult Index(Order order)
        {
           
            if (!ModelState.IsValid)
            {
                return BadRequest(string.Join("\n", ModelState.Values.SelectMany(value => value.Errors)
                    .Select(error => error.ErrorMessage)));
            }
            
            
            order.OrderNo = Utility.Utility.GetRandomNumber();
            return Json(new { orderNo = order.OrderNo});
            
        }

    }
   
}
