using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Transaction.Domain.AggreagatesModels.BuyerAggregate;
using Transaction.Domain.SeedWork;
using Transaction.Infrastructure.Database;

namespace Transaction.Infrastructure.Repository
{
    public class BuyerRepository:IBuyerRepository
    {
        private readonly BuyerDbContext _buyerDbContext;

        IUnitOfWork IRepository<Buyer>.UnitOfWork => throw new NotImplementedException();

        public Buyer Add(Buyer buyer)
        {
            return _buyerDbContext.Buyers.Add(buyer).Entity;
        }
        //public async Task<Buyer> GetAsync(int buyerId)
        //{
        //    var buyer = await _buyerDbContext.Buyers.FindAsync(buyerId);
        //    if (buyer != null)
        //    {
        //        //await _buyerDbContext.Entry(buyer)
        //        //    .Collection(i => i.OrderItems).LoadAsync();
        //        await _buyerDbContext.Entry(buyer)
        //            .Reference(i => i.BuyerTransactionStatus).LoadAsync();
        //        //await _buyerDbContext.Entry(buyer)
        //        //    .Reference(i => i.Address).LoadAsync();
        //    }

        //    return buyer;
        //}
    }
}
