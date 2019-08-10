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

    //by lalji
    public class TransactionController : Controller
    {
        [Route("Buy")]
        [HttpPost]
        public async Task<IActionResult> BuyTrade([FromBody] decimal Price,decimal Quantity)
        {
            
            try
            {
                //logic type here.....
            }
            catch
            {
            }
            return Ok();
        }

        [Route("sale")]
        [HttpPost]
        public async Task<IActionResult> saleTrade([FromBody]string TransactionId)
        {
            try
            {
                //logic
            }
            catch
            {

            }
            return Ok();
        }
    }
}
