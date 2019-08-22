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
        public async Task<TransactionResponse> Execute(BuyerData buy)
        {
            Console.WriteLine(buy.BuyPrice + " : " + buy.BuyQuantity + " : " + buy.InsertTime);
            Console.WriteLine("------------------------------------------------------");
            try
            {
                await _buyerRepository.AddBuyerData(buy);
                List<SellerData> SellerList = await _sellerRepository.GetGreterSellerPriceListFromBuyerPrice(buy.BuyPrice);


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
                            else
                            {
                                sell.TransactionStatus = TransactionStatus.OperatorFail;
                                buy.TransactionStatus = TransactionStatus.OperatorFail;
                            }
                        }
                        else
                        {
                            sell.TransactionStatus = TransactionStatus.SystemFail;
                            buy.TransactionStatus = TransactionStatus.SystemFail;
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
                            sell.TransactionStatus = TransactionStatus.Hold;
                            if (buy.RemainingQuantity == 0)
                            {
                                buy.TransactionStatus = TransactionStatus.Success;
                            }
                            else
                            {
                                buy.TransactionStatus = TransactionStatus.Hold;
                            }
                        }
                        else
                        {
                            sell.TransactionStatus = TransactionStatus.Success;
                        }
                    }
                    await _sellerRepository.UpdateSellerData(sell);
                    await _buyerRepository.UpdateBuyerData(buy);
                    await _ledgerRepository.AddLedgerData(sell, buy, Quantities);
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

