using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Transaction.API.Application.Validation
{
    
        public enum TransactionStatus
    {
            Pending = 0,
            Success = 1,
            OperatorFail = 2,
            SystemFail = 3,
            Hold = 4,
            Refunded = 5
        }
    
}
