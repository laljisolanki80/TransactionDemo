using System;
using System.Collections.Generic;
using System.Text;

namespace Transaction.Domain.AggreagatesModels.Aggregate
{
    public enum TransactionStatus
    {
        Hold = 0,
        Success = 1,
        OperatorFail = 2,
        SystemFail = 3,
        PartialSettle = 4,
        Refunded = 5,
        CancelTransaction=6


    }
}
