using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Transaction.Domain.IService
{
    public interface ICancelTransactionService
    {
        Task CancelTransaction(string TransactionId);
    }
}
