using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Transaction.Domain.AggreagatesModels.Aggregate;

namespace Transaction.Domain.IRepository
{
   public interface ISellerRepository
    {
        Task AddSellerData(SellerData sellerData);
        Task<List<SellerData>> GetGreaterSellerPriceListFromBuyerPrice(decimal BuyerPrice);
        Task UpdateSellerData(SellerData sell);

        Task<SellerData> GetSellerById(TransactionCancelModel transactionCancelModel);//for cancel transaction by lalji 23/08/2018

    }
}
