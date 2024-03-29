﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Transaction.Domain.AggreagatesModels.Aggregate;
using Transaction.Domain.SeedWork;

namespace Transaction.Domain.IRepository
{
    public interface IBuyerRepository:IRepository<BuyerData>
    {
        Task AddBuyerData(BuyerData buyerData);
        Task<List<BuyerData>> GetGreaterBuyerPriceListFromSellerPrice(decimal SellerPrice);
        Task UpdateBuyerData(BuyerData buy);
        
        Task<BuyerData> GetBuyerById(TransactionCancelModel transactionCancelModel); //by lalji for cancel order 
    }
}
