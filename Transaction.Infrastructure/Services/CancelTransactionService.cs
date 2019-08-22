using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Transaction.Domain.IService;

namespace Transaction.Infrastructure.Services
{
    class CancelTransactionService: ICancelTransactionService
    {
        public Task CancelTransaction(string TransactionId)
        {
            throw new NotImplementedException();
        }

     }
}
