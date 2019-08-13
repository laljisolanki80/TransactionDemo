using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Transaction.API.Application.Queries
{
    public interface IBuyerQueries
    {
        Task<Buyer> GetBuyerAsync();
    }
}
