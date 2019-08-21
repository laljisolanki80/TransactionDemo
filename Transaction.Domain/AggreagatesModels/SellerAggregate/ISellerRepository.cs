using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Transaction.Domain.SeedWork;

namespace Transaction.Domain.AggreagatesModels.SellerAggregate
{
    public interface ISellerRepository:IRepository<Seller>
    {
        Seller Add(Seller seller);
        Task<Seller> FindAsync(decimal price);
    }
}
