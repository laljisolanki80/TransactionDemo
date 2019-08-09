using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Transaction.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Transaction")]
    public class TransactionController : Controller
    {
        [Route("CreateTransaction")]
        [HttpPost]
        public async Task<IActionResult> CreateNewTransaction ([FromBody]int AccountId, string Name, int Amount)
        {
            if (AccountId==null)
            {

                return BadRequest();
            }
               return Ok("done");      
        }

      
    }
}
