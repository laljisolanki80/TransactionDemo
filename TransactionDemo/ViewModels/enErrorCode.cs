using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransactionDemo.ViewModels
{
    public enum enErrorCode
    {
        Success = 10001,
        InternalError = 10009,
        TransactionNotFoundError = 10002
    }
}
