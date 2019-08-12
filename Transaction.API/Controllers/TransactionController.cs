using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Transaction.API.Application.Command;

namespace Transaction.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Transaction")]

    //by Akshay
    public class TransactionController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<TransactionController> _logger;
        public TransactionController(IMediator mediator, ILogger<TransactionController> logger)
        {
            _mediator = mediator;// ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [Route("BuyTrade")]
        [HttpPost]
        public async Task<bool> BuyTrade([FromBody]BuyTransactionCommand buyTransactionCommand)
        {
            return await _mediator.Send(buyTransactionCommand);
            //try
            //{
            //}
            //catch
            //{
            //}
            //return Ok("success done");
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
