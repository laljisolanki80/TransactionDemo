using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Transaction.Domain.AggreagatesModels.Aggregate;
using Transaction.Domain.Enum;
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
        //public async Task<TransactionResponse> Execute(SellerData sell)
       public async Task<TransactionResponse> Execute(TransactionModel transactionModel)
        {
            SellerData sell = new SellerData(transactionModel.Price, transactionModel.Quantity);

            Console.WriteLine(sell.SellPrice + " : " + sell.SellQuantity + " : " + sell.InsertTime);
            Console.WriteLine("------------------------------------------------------");
            try
            {
                await _sellerRepository.AddSellerData(sell);

                List<BuyerData> BuyerList = await _buyerRepository.GetGreterBuyerPriceListFromSellerPrice(sell.SellPrice);
                foreach (var buy in BuyerList)
                {
                    decimal Quantities = 0.0m;
                    if (buy.RemainingQuantity >= sell.RemainingQuantity)
                    {
                        Quantities = sell.RemainingQuantity;
                    }
                    if (sell.RemainingQuantity > buy.RemainingQuantity)
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
                                //sell.TransactionStatus = TransactionStatus.Success;
                                sell.StatusChangeToSettleStatus();
                            }
                            if (sell.RemainingQuantity > 0)
                            {
                                //sell.TransactionStatus = TransactionStatus.Hold;
                                sell.StatusChangeToOnHoldStatus();
                            }
                            if (buy.RemainingQuantity == 0)
                            {
                                //buy.TransactionStatus = TransactionStatus.Success;
                                buy.StatusChangeToSettleStatus();
                            }
                            if (buy.RemainingQuantity > 0)
                            {
                                //buy.TransactionStatus = TransactionStatus.Hold;
                                buy.StatusChangeToOnHoldStatus();
                            }
                        }
                        else
                        {
                           // sell.TransactionStatus = TransactionStatus.SystemFail;
                            sell.StatusChangeToFailedStatus();
                            // buy.TransactionStatus = TransactionStatus.SystemFail;
                            buy.StatusChangeToFailedStatus();
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
                            //buy.TransactionStatus = TransactionStatus.Hold;
                            buy.StatusChangeToOnHoldStatus();
                            if (sell.RemainingQuantity == 0)
                            {
                                //sell.TransactionStatus = TransactionStatus.Success;
                                sell.StatusChangeToSettleStatus();
                            }
                            else
                            {
                                //sell.TransactionStatus = TransactionStatus.Hold;
                                sell.StatusChangeToOnHoldStatus();
                            }
                        }
                        else
                        {
                            //buy.TransactionStatus = TransactionStatus.Success;
                            buy.StatusChangeToFailedStatus();
                        }
                    }
                    await _sellerRepository.UpdateSellerData(sell);
                    await _buyerRepository.UpdateBuyerData(buy);
                    await _ledgerRepository.AddLedgerData(sell, buy, Quantities);
                }
                TransactionResponse transactionResponse = new TransactionResponse();
                transactionResponse.UniqId = sell.SellerId.ToString();
                transactionResponse.StatusCode = (int)sell.TransactionStatus;
                transactionResponse.StatusMessage = sell.TransactionStatus.ToString();
                return transactionResponse;
            }
            catch(Exception)
            {
                return (new TransactionResponse { ErrorCode = enErrorCode.InternalError });
                //TransactionResponse transactionResponse = new TransactionResponse();
                //transactionResponse.ErrorCode = enErrorCode.TryAgain;
                //return transactionResponse;
            }
            
        }
    }
}
