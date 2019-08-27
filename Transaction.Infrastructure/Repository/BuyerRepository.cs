using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction.Domain.AggreagatesModels.Aggregate;
using Transaction.Domain.IRepository;
using Transaction.Domain.SeedWork;
using Transaction.Infrastructure.DataBase;

namespace Transaction.Infrastructure.Repository
{
    public class BuyerRepository : IBuyerRepository
    {
        public TransactionDbContext _transactionDbContext;
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _transactionDbContext;
            }
        }
        public BuyerRepository(TransactionDbContext transactionDbContext)
        {
            _transactionDbContext = transactionDbContext;
        }

       
        public async Task AddBuyerData(BuyerData buyerData)
        {
            _transactionDbContext.BuyerDatas.Add(buyerData);
            await _transactionDbContext.SaveChangesAsync();
        }

        public async Task<List<BuyerData>> GetGreaterBuyerPriceListFromSellerPrice(decimal SellerPrice)
        {
            try
            {
                var compare = from buyRaw in _transactionDbContext.BuyerDatas
                              where buyRaw.BuyPrice >= SellerPrice && buyRaw.RemainingQuantity > 0
                              orderby buyRaw.InsertTime
                              select buyRaw;

                if (compare != null)
                {
                    var BuyerList = compare.ToList();                 
                    return await Task.FromResult(BuyerList);                 
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
        public async Task UpdateBuyerData(BuyerData buy)
        {
            _transactionDbContext.BuyerDatas.Update(buy);
            //await _transactionDbContext.SaveChangesAsync();
            await _transactionDbContext.SaveEntitiesAsync();
        }

        public async Task<BuyerData> GetBuyerById(TransactionCancelModel transactionCancelModel)
        {
            var find = from buyRaw in _transactionDbContext.BuyerDatas
                       where buyRaw.BuyId == Guid.Parse(transactionCancelModel.Id) && buyRaw.TransactionStatus == TransactionStatus.Hold
                       
                       select buyRaw;
            
            return await Task.FromResult(find.FirstOrDefault());
        }
    }
}
