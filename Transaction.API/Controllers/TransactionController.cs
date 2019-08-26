using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Transaction.Domain.AggreagatesModels.Aggregate;
using Transaction.Domain.IRepository;
using Transaction.Domain.IService;

namespace Transaction.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Transaction")]

    public class TransactionController : ControllerBase
    {
        private readonly ISellerService _sellerService;
        private readonly IBuyerService _buyerService;
    
        public TransactionController(ISellerService sellerService,IBuyerService buyerService)
        {
            _sellerService = sellerService;
            _buyerService = buyerService;
        }
        [Route("SellTrade")]
        [HttpPost]
        public async Task<IActionResult> SellTrade([FromBody]TransactionModel transactionModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            TransactionResponse response = await _sellerService.Execute(transactionModel);
                  
            return Ok(response);
        }

        [Route("BuyTrade")]
        [HttpPost]
        public async Task<IActionResult> BuyTrade([FromBody]TransactionModel transactionModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            TransactionResponse response=await _buyerService.Execute(transactionModel);
            return Ok(response);
        }

        [HttpPost]
        [Route("BuyerCancel")]
        public async Task<IActionResult> BuyerCancelTransaction([FromBody]TransactionCancelModel transactionCancelModel)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            BizResponse response = await _buyerService.CancelTransaction(transactionCancelModel);

            return Ok(response);
        }

        [HttpPost]
        [Route("SellerCancel")]
        public async Task<IActionResult> SellerCancelTransaction([FromBody] TransactionCancelModel transactionCancelModel)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            BizResponse response = await _sellerService.CancelTransaction(transactionCancelModel);
            return Ok(response);
        }
    }
}
