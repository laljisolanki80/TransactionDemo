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

        public async Task<BizResponse> CancelTransaction(TransactionCancelModel transactionCancelModel)
        {
            //var seller = await _sellerRepository.GetSellerById(cancelSellerTransaction);
            var seller = await _sellerRepository.GetSellerById(transactionCancelModel);
            try
            {
                if (seller == null)
                {
                    BizResponse bizResponse = new BizResponse();
                    bizResponse.ErrorCode = Domain.Enum.enErrorCode.TransactionNotFoundError;
                    bizResponse.StatusCode = (int)TransactionStatus.OperatorFail;
                    bizResponse.StatusMessage = "transaction not found";

                    return bizResponse;
                }
                else
                {
                    seller.StatusChangeToCancleStatus();
                    seller.InsertDateAndTime();
                    await _sellerRepository.UpdateSellerData(seller);

                    BizResponse bizResponse = new BizResponse();
                    bizResponse.ErrorCode = Domain.Enum.enErrorCode.Success;
                    bizResponse.StatusCode = (int)TransactionStatus.Success;
                    bizResponse.StatusMessage = "Transaction cancelled successfully";
                    return bizResponse;
                }
            }
            catch(Exception)
            {
                return (new BizResponse {ErrorCode = enErrorCode.InternalError,StatusMessage="Internel error",StatusCode=(int)TransactionStatus.OperatorFail }); 
            }
        }

    
        public async Task<TransactionResponse> Execute(TransactionModel transactionModel)
        {
            SellerData sell = new SellerData(transactionModel.Price, transactionModel.Quantity);
                        
            try
            {
                await _sellerRepository.AddSellerData(sell);

                List<BuyerData> BuyerList = await _buyerRepository.GetGreaterBuyerPriceListFromSellerPrice(sell.SellPrice);
                foreach (var buy in BuyerList)
                {
                    decimal Quantities = 0.0m;
                    if(sell.RemainingQuantity>=buy.RemainingQuantity)
                    {
                        Quantities = buy.RemainingQuantity;
                    }
                    else
                    {
                        Quantities = sell.RemainingQuantity;
                    }
                    sell.RemainingQuantity = sell.RemainingQuantity - Quantities;
                    sell.SettledQuantity = sell.SettledQuantity + Quantities;
                    buy.RemainingQuantity = buy.RemainingQuantity - Quantities;
                    buy.SettledQuantity = buy.SettledQuantity + Quantities;

                    if(sell.RemainingQuantity==0)
                    {
                        sell.StatusChangeToSettleStatus();
                    }
                    else
                    {
                        sell.StatusChangeToPartialSettleStatus();
                    }
                    if(buy.RemainingQuantity==0)
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
                    
                    if (sell.RemainingQuantity == 0)
                    {                        
                        break;
                    }
                    
                }
                TransactionResponse transactionResponse = new TransactionResponse();
                transactionResponse.UniqId = sell.SellerId.ToString();
                transactionResponse.StatusCode = (int)sell.TransactionStatus;
                transactionResponse.StatusMessage = sell.TransactionStatus.ToString();
                return transactionResponse;
            }
            catch(Exception)
            {
                return (new TransactionResponse { ErrorCode = enErrorCode.InternalError,StatusCode=(int)TransactionStatus.SystemFail,StatusMessage=TransactionStatus.SystemFail.ToString() });                
            }
            
        }
    }
}
