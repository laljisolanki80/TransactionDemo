using System;
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

namespace Transaction.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Transaction")]

    public class TransactionController : Controller
    {
        private ISellerRepository _sellerRepository;
        private IBuyerRepository _buyerRepository;
        public TransactionController(ISellerRepository sellerRepository,IBuyerRepository buyerRepository)
        {
            _sellerRepository = sellerRepository;
            _buyerRepository = buyerRepository;
        }
        [Route("SaleTrade")]
        [HttpGet]
        public async Task SaleTrade([FromBody]SellerDataModel sellerDataModel)
        {
            SellerData sellerData = new SellerData(sellerDataModel.SellPrice, sellerDataModel.SellQuantity,
                sellerDataModel.SettledQuantity, sellerDataModel.RemainingQuantity);

            _sellerRepository.AddSellerData(sellerData);
            _sellerRepository.GetSellerTransactionAsync();
        }

        [Route("BuyTrade")]
        [HttpPost]
        public async Task BuyTrade([FromBody]BuyerDataModel buyerDataModel)
        {
            BuyerData buyerData = new BuyerData(buyerDataModel.BuyPrice, buyerDataModel.BuyQuantity,
                buyerDataModel.SettledQuantity, buyerDataModel.RemainingQuantity);

            _buyerRepository.AddBuyerData(buyerData);
            _buyerRepository.GetBuyerTransactionAsync();
            
        }
    }
}
