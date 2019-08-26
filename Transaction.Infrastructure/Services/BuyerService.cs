using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Transaction.Domain.AggreagatesModels.Aggregate;
using Transaction.Domain.IRepository;
using Transaction.Domain.IService;

namespace Transaction.Infrastructure.Service
{
    public class BuyerService : IBuyerService
    {
        private readonly ISellerRepository _sellerRepository;
        private readonly IBuyerRepository _buyerRepository;
        private readonly ILedgerRepository _ledgerRepository;
        public BuyerService(ISellerRepository sellerRepository,
            IBuyerRepository buyerRepository,
            ILedgerRepository ledgerRepository)
        {
            _sellerRepository = sellerRepository;
            _buyerRepository = buyerRepository;
            _ledgerRepository = ledgerRepository;
        }

        
        public async Task<TransactionResponse> CancelTransaction(TransactionCancelModel transactionCancelModel) //add by lalji 23/08/2019
        {
           var buyer = await _buyerRepository.GetBuyerById(transactionCancelModel);

        
            buyer.StatusChangeToCancleStatus();

            await _buyerRepository.UpdateBuyerData(buyer);

            TransactionResponse transactionResponse = new TransactionResponse();
            transactionResponse.StatusCode =(int) buyer.TransactionStatus;
            transactionResponse.StatusMessage = "transaction cancelled successfully";

            return transactionResponse;
        }

        //public async Task<TransactionResponse> Execute(BuyerData buy)
        public async Task<TransactionResponse> Execute(TransactionModel transactionModel)
        {
            BuyerData buy = new BuyerData(transactionModel.Price, transactionModel.Quantity);

            Console.WriteLine(buy.BuyPrice + " : " + buy.BuyQuantity + " : " + buy.InsertTime);
            Console.WriteLine("------------------------------------------------------");
            try
            {
                await _buyerRepository.AddBuyerData(buy);
                List<SellerData> SellerList = await _sellerRepository.GetGreaterSellerPriceListFromBuyerPrice(buy.BuyPrice);


                foreach (var sell in SellerList)
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
                    if (buy.RemainingQuantity >= sell.RemainingQuantity)
                    {
                        buy.RemainingQuantity -= sell.RemainingQuantity;
                        buy.SettledQuantity += sell.RemainingQuantity;
                        sell.SettledQuantity = sell.SettledQuantity + sell.RemainingQuantity;
                        sell.RemainingQuantity = sell.SellQuantity - sell.SettledQuantity;
                        if (sell.RemainingQuantity == 0 || sell.RemainingQuantity > 0
                            || buy.RemainingQuantity > 0 || buy.RemainingQuantity == 0)
                        {
                            if (sell.RemainingQuantity == 0)
                            {
                                //sell.TransactionStatus = TransactionStatus.Success;
                                sell.StatusChangeToSettleStatus();
                            }
                            if (sell.RemainingQuantity > 0)
                            {
                                //sell.TransactionStatus = TransactionStatus.Hold;
                                sell.StatusChangeToPartialSettleStatus();
                            }
                            if (buy.RemainingQuantity == 0)
                            {
                                //buy.TransactionStatus = TransactionStatus.Success;
                                buy.StatusChangeToSettleStatus();
                            }
                            if (buy.RemainingQuantity > 0)
                            {
                                //buy.TransactionStatus = TransactionStatus.Hold;
                                buy.StatusChangeToPartialSettleStatus();
                            }
                        }
                        else
                        {
                            //sell.TransactionStatus = TransactionStatus.SystemFail;
                            sell.StatusChangeToFailedStatus();
                            //buy.TransactionStatus = TransactionStatus.SystemFail;
                            buy.StatusChangeToFailedStatus();
                        }
                    }
                    if (buy.RemainingQuantity < sell.RemainingQuantity)
                    {
                        sell.RemainingQuantity -= buy.RemainingQuantity;
                        sell.SettledQuantity = sell.SellQuantity - sell.RemainingQuantity;
                        if (sell.RemainingQuantity > 0)
                        {
                            buy.SettledQuantity += buy.RemainingQuantity;
                            buy.RemainingQuantity = 0;
                            //sell.TransactionStatus = TransactionStatus.Hold;
                            sell.StatusChangeToOnHoldStatus();
                            if (buy.RemainingQuantity == 0)
                            {
                                //buy.TransactionStatus = TransactionStatus.Success;
                                buy.StatusChangeToSettleStatus();
                            }
                            else
                            {
                                //buy.TransactionStatus = TransactionStatus.Hold;
                                buy.StatusChangeToOnHoldStatus();
                            }
                        }
                        if (buy.BuyPrice > sell.SellPrice)
                        {
                            buy.StatusChangeToOnHoldStatus();
                        }
                        else
                        {
                            //sell.TransactionStatus = TransactionStatus.Success;
                            sell.StatusChangeToFailedStatus();
                        }
                    }
                    await _sellerRepository.UpdateSellerData(sell);                    
                    await _buyerRepository.UpdateBuyerData(buy);
                    if (Quantities != 0)
                    {
                        await _ledgerRepository.AddLedgerData(sell, buy, Quantities);
                    }
                    else
                    {
                        buy.StatusChangeToOnHoldStatus();
                    }
                }
                TransactionResponse transactionResponse = new TransactionResponse();
                transactionResponse.UniqId = buy.BuyId.ToString();
                transactionResponse.StatusCode = (int)buy.TransactionStatus;
                transactionResponse.StatusMessage = buy.TransactionStatus.ToString();
                
                return transactionResponse;
            }
            catch(Exception ex)
            {
                return null;
            }
            

            
        }
    }
}

