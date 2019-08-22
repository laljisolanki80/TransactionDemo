using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Transaction.Domain.AggreagatesModels.Aggregate;

namespace Transaction.Domain.IRepository
{
    public interface ILedgerRepository
    {
        Task AddLedgerData(SellerData sell, BuyerData buy,decimal Quantities);
    }
}
