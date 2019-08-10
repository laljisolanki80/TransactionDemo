using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Transaction.API.Application.Command;

namespace Transaction.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Transaction")]

    //by lalji
    public class TransactionController : Controller
    {
        [Route("Buy")]
        [HttpPost]
        public async Task<IActionResult> BuyTrade([FromBody]BuyTransactionCommand buyTransactionCommand)
        {            
            try
            {
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
