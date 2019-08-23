﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Transaction.API.Application.Models;
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
        private readonly ICancelTransactionService _cancelTransactionService;

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
            SellerData sellerData = new SellerData(transactionModel.Price, transactionModel.Quantity);
            //TransactionResponse response = await _sellerService.Execute(transactionModel);
            TransactionResponse response = await _sellerService.Execute(sellerData);            
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
            BuyerData buyerData = new BuyerData(transactionModel.Price, transactionModel.Quantity);
            TransactionResponse response=await _buyerService.Execute(buyerData);
            return Ok(response);
        }
        //[HttpPost]
        ////[Route("CancelTrade")]
        //public async Task<IActionResult> CancelOrder([FromBody] string TransactionId)
        //{

        //    if (TransactionId != null)
        //    {
        //        await _cancelTransactionService.Execute(TransactionId)
        //    }

        //    return Ok()
        //}
    }
}
