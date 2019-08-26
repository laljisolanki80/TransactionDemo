using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction.Domain.AggreagatesModels.Aggregate;
using Transaction.Domain.IRepository;
using Transaction.Infrastructure.DataBase;

namespace Transaction.Infrastructure.Repository
{
    public class SellerRepository : ISellerRepository
    {
        private TransactionDbContext _transactionDbContext;
        public SellerRepository(TransactionDbContext transactionDbContext)
        {
            _transactionDbContext = transactionDbContext;
        }
        public async Task AddSellerData(SellerData sellerData)
        {
            _transactionDbContext.SellerDatas.Add(sellerData);
            await _transactionDbContext.SaveChangesAsync();
        }
        public async Task<List<SellerData>> GetGreterBuyerPriceListFromSellerPrice(decimal BuyerPrice)
        {
            var compare = from sellRaw in _transactionDbContext.SellerDatas
                          where sellRaw.SellPrice <= BuyerPrice
                          orderby sellRaw.InsertTime
                          select sellRaw;

            var Buyerlist = compare.ToList();
            return await Task.FromResult(Buyerlist);
        }

        public async Task<List<SellerData>> GetGreaterSellerPriceListFromBuyerPrice(decimal BuyerPrice)
        {
            try
            {
                var compare = from sellRaw in _transactionDbContext.SellerDatas
                              where sellRaw.SellPrice <= BuyerPrice
                              orderby sellRaw.InsertTime
                              select sellRaw;
                if (compare != null)
                {
                    var SellerList = compare.ToList();
                    return await Task.FromResult(SellerList);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }       
            
            
        }

        
        public async Task UpdateSellerData(SellerData sell)
        {
            _transactionDbContext.SellerDatas.Update(sell);
            //await _transactionDbContext.SaveChangesAsync();
            await _transactionDbContext.SaveEntitiesAsync();
        }

       public async Task<SellerData> GetSellerById(TransactionCancelModel transactionCancelModel)
        {
            var find = from sellRaw in _transactionDbContext.SellerDatas
                       where sellRaw.SellerId == Guid.Parse(transactionCancelModel.Id) && sellRaw.TransactionStatus == TransactionStatus.Hold

                       select sellRaw;

            return await Task.FromResult(find.FirstOrDefault());
        }
    }
}
