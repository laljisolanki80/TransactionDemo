using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction.Domain.AggreagatesModels.BuyerAggregate;
using Transaction.Domain.SeedWork;
using Transaction.Infrastructure.Database;

namespace Transaction.Infrastructure.Repository
{
    public class BuyerRepository:IBuyerRepository
    {
        private readonly TransactionDbContext _transactionDbContext;

        IUnitOfWork IRepository<Buyer>.UnitOfWork => throw new NotImplementedException();

        public Buyer Add(Buyer buyer)
        {
            return _transactionDbContext.Buyers.Add(buyer).Entity;
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
        public async Task<Buyer> FindAsync(decimal price)
        {
            var buyer = await _transactionDbContext.Buyers
                .Include(b => b.GetBuyerId)
                .Where(b => b.Price == price)
                .SingleOrDefaultAsync();

            return buyer;
        }
    }
}
