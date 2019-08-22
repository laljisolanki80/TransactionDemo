using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Transaction.Domain.AggreagatesModels.Aggregate;
using Transaction.Domain.IRepository;
using Transaction.Domain.IService;

namespace Transaction.Infrastructure.Service
{
    public class SellerService : ISellerService
    {
        private readonly ISellerRepository _sellerRepository;
        private readonly IBuyerRepository _buyerRepository;
        private readonly ILedgerRepository _ledgerRepository;
        public SellerService(ISellerRepository sellerRepository, 
            IBuyerRepository buyerRepository,
            ILedgerRepository ledgerRepository)
        {
            _sellerRepository = sellerRepository;
            _buyerRepository = buyerRepository;
            _ledgerRepository = ledgerRepository;
        }
        public async Task<TransactionResponse> Execute(SellerData sell)
        {
            Console.WriteLine(sell.SellPrice + " : " + sell.SellQuantity + " : " + sell.InsertTime);
            Console.WriteLine("------------------------------------------------------");

            await _sellerRepository.AddSellerData(sell);

            List<BuyerData> BuyerList = await _buyerRepository.GetGreterBuyerPriceListFromSellerPrice(sell.SellPrice);
            foreach (var buy in BuyerList)
            {
                decimal Quantities = 0.0m;
                if (buy.RemainingQuantity >= sell.RemainingQuantity)
                {
                    Quantities = sell.RemainingQuantity;
                }
                if (sell.RemainingQuantity >= buy.RemainingQuantity)
                {
                    Quantities = buy.RemainingQuantity;
                }
                if (sell.RemainingQuantity >= buy.RemainingQuantity)
                {
                    sell.RemainingQuantity -= buy.RemainingQuantity;
                    sell.SettledQuantity += buy.RemainingQuantity;
                    buy.SettledQuantity = buy.SettledQuantity + buy.RemainingQuantity;
                    buy.RemainingQuantity = buy.BuyQuantity - buy.SettledQuantity;
                    if (sell.RemainingQuantity == 0 || sell.RemainingQuantity > 0
                        || buy.RemainingQuantity > 0 || buy.RemainingQuantity == 0)
                    {
                        if (sell.RemainingQuantity == 0)
                        {
                            sell.TransactionStatus = TransactionStatus.Success;
                        }
                        if (sell.RemainingQuantity > 0)
                        {
                            sell.TransactionStatus = TransactionStatus.Hold;
                        }
                        if (buy.RemainingQuantity == 0)
                        {
                            buy.TransactionStatus = TransactionStatus.Success;
                        }
                        if (buy.RemainingQuantity > 0)
                        {
                            buy.TransactionStatus = TransactionStatus.Hold;
                        }                        
                    }
                    else
                    {
                        sell.TransactionStatus = TransactionStatus.SystemFail;
                        buy.TransactionStatus = TransactionStatus.SystemFail;
                    }
                }
                if (sell.RemainingQuantity < buy.RemainingQuantity)
                {
                    buy.RemainingQuantity -= sell.RemainingQuantity;
                    buy.SettledQuantity = buy.BuyQuantity - buy.RemainingQuantity;
                    if (buy.RemainingQuantity > 0)
                    {
                        sell.SettledQuantity += sell.RemainingQuantity;
                        sell.RemainingQuantity = 0;
                        buy.TransactionStatus = TransactionStatus.Hold;
                        if (sell.RemainingQuantity == 0)
                        {
                            sell.TransactionStatus = TransactionStatus.Success;
                        }
                        else
                        {
                            sell.TransactionStatus = TransactionStatus.Hold;
                        }
                    }
                    else
                    {
                        buy.TransactionStatus = TransactionStatus.Success;
                    }
                }
                await _sellerRepository.UpdateSellerData(sell);
                await _buyerRepository.UpdateBuyerData(buy);
                await _ledgerRepository.AddLedgerData(sell, buy, Quantities);
            }
            TransactionResponse transactionResponse = new TransactionResponse();
            transactionResponse.UniqId = sell.SellerId.ToString();
            transactionResponse.StatusCode = sell.TransactionStatus.ToString();
            transactionResponse.StatusMessage = ((TransactionStatus)sell.TransactionStatus).ToString();
            return transactionResponse;
        }
    }
}
