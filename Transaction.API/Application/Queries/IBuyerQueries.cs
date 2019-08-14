using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transaction.API.Application.Models;

namespace Transaction.API.Application.Queries
{
    public interface IBuyerQueries
    {
        Task<Buyer> GetTransactionAsync(TransactionModel transactionModel);
    }
}
