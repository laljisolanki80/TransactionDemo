﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Transaction.Domain.Validation
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
