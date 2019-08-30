using System;
using System.Collections.Generic;
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


        public async Task<BizResponse> CancelTransaction(TransactionCancelModel transactionCancelModel) //add by lalji 23/08/2019
        {
            var buyer = await _buyerRepository.GetBuyerById(transactionCancelModel);
            try
            {
                if (buyer == null)
                {
                    BizResponse bizResponse = new BizResponse();
                    bizResponse.ErrorCode = Domain.Enum.enErrorCode.TransactionNotFoundError;
                    bizResponse.StatusCode = (int)TransactionStatus.ProviderFail;
                    bizResponse.StatusMessage = "transaction not found";

                    return bizResponse;
                }
                else
                {
                    buyer.StatusChangeToCancleStatus();
                    buyer.InsertDateAndTime();
                    await _buyerRepository.UpdateBuyerData(buyer);

                    BizResponse bizResponse = new BizResponse();
                    bizResponse.ErrorCode = Domain.Enum.enErrorCode.Success;
                    bizResponse.StatusCode = (int)TransactionStatus.Success;
                    bizResponse.StatusMessage = "Transaction cancelled successfully";
                    return bizResponse;
                }

            }
            catch (Exception)
            {
                return (new BizResponse { ErrorCode = Domain.Enum.enErrorCode.InternalError, StatusMessage = "Internel error", StatusCode = (int)TransactionStatus.ProviderFail});
            }
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

                    sell.RemainingQuantity -= Quantities;
                    sell.SettledQuantity += Quantities;
                    buy.RemainingQuantity -= Quantities;
                    buy.SettledQuantity += Quantities;

                    if (sell.RemainingQuantity == 0)
                    {
                        sell.StatusChangeToSettleStatus();
                    }
                    else
                    {
                        sell.StatusChangeToOnHoldStatus();
                    }
                    if (buy.RemainingQuantity == 0)
                    {
                        buy.StatusChangeToSettleStatus();
                    }
                    else
                    {
                        buy.StatusChangeToOnHoldStatus();
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
                return (new TransactionResponse { ErrorCode = enErrorCode.InternalError, StatusCode = (int)TransactionStatus.Validationfail, StatusMessage = TransactionStatus.Validationfail.ToString() });
            }
        }
    }
}

