using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Transaction.Domain.SeedWork;

namespace Transaction.Domain.AggreagatesModels.BuyerAggregate
{
    public interface IBuyerRepository:IRepository<Buyer>
    {
        Buyer Add(Buyer buyer);
        Task<Buyer> GetAsync(int buyerId);

    }
}
