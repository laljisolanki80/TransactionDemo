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
            transactionResponse.StatusMessage = buyer.TransactionStatus.ToString();
            transactionResponse.UniqId = buyer.BuyId.ToString();

            return transactionResponse;
        }

        public async Task<TransactionResponse> Execute(TransactionModel transactionModel)
        {
            BuyerData buy = new BuyerData(transactionModel.Price, transactionModel.Quantity);

            try
            {
                await _buyerRepository.AddBuyerData(buy);
                List<SellerData> SellerList = await _sellerRepository.GetGreaterSellerPriceListFromBuyerPrice(buy.BuyPrice);
                
                foreach (var sell in SellerList)
                {
                    decimal Quantities = 0.0m;
                    if(buy.RemainingQuantity>=sell.RemainingQuantity)
                    {
                        Quantities = sell.RemainingQuantity;
                    }
                    else
                    {
                        Quantities = buy.RemainingQuantity;
                    }

                    sell.RemainingQuantity = sell.RemainingQuantity - Quantities;
                    sell.SettledQuantity = sell.SettledQuantity + Quantities;
                    buy.RemainingQuantity = buy.RemainingQuantity - Quantities;
                    buy.SettledQuantity = buy.SettledQuantity + Quantities;

                    if (sell.RemainingQuantity == 0)
                    {
                        sell.StatusChangeToSettleStatus();
                    }
                    else
                    {
                        sell.StatusChangeToPartialSettleStatus();
                    }
                    if (buy.RemainingQuantity == 0)
                    {
                        buy.StatusChangeToSettleStatus();
                    }
                    else
                    {
                        buy.StatusChangeToPartialSettleStatus();
                    }
                    
                    await _sellerRepository.UpdateSellerData(sell);                    
                    await _buyerRepository.UpdateBuyerData(buy);
                    await _ledgerRepository.AddLedgerData(sell, buy, Quantities);
                    
                    if(buy.RemainingQuantity==0)
                    {
                        break;
                    }
                }
                TransactionResponse transactionResponse = new TransactionResponse();
                transactionResponse.UniqId = buy.BuyId.ToString();
                transactionResponse.StatusCode = (int)buy.TransactionStatus;
                transactionResponse.StatusMessage = buy.TransactionStatus.ToString();
                
                return transactionResponse;
            }
            catch (Exception)
            {
                return (new TransactionResponse { ErrorCode = enErrorCode.InternalError, StatusCode = (int)TransactionStatus.SystemFail, StatusMessage = TransactionStatus.SystemFail.ToString() });
            }
        }
    }
}

