using BankingControllerExample.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace BankingControllerExample.Controllers
{
    public class AccountController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            return Ok("Welcome to the best bank!");
        }


        [Route("account-details")]    
        public IActionResult GetAccountDetails() => Json(new Account());

        [Route("account-statement")]
        public IActionResult GetDummyFile() => File("/cert.pdf", "application/pdf");

        [Route("get-current-balance/{accountNumber:int:min(1)?}")]
        public IActionResult GetCurrentBalance()
        {
            var acctNum = Convert.ToInt32(Request.RouteValues["accountNumber"]);

            if( acctNum == 0)
            {
                return NotFound("Account number should be provided");
            }
            else if (acctNum != 1001)
            {
                return BadRequest("Account number should be 1001");
            }
            else
            {
                return Ok(5000);
            }

        }
    }
}
