using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction.Domain.AggreagatesModels.BuyerAggregate;
using Transaction.Domain.AggreagatesModels.SellerAggregate;
using Transaction.Domain.SeedWork;
using Transaction.Infrastructure.Database;

namespace Transaction.Infrastructure.Repository
{
    public class SallerRepository:ISellerRepository
    {
        private readonly TransactionDbContext _transactionDbContext;

        public IUnitOfWork UnitOfWork => throw new NotImplementedException();

        public Seller Add(Seller seller)
        {
            return _transactionDbContext.Sellers.Add(seller).Entity;
        }

        public async Task<Seller> FindAsync(decimal price)
        {
            var seller = await _transactionDbContext.Sellers
                .Include(b => b.GetSellerId)
                .Where(b => b.Price == price)
                .SingleOrDefaultAsync();

            return seller;
        }

    }
}
