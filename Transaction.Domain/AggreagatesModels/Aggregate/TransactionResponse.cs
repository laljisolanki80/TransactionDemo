using System;
using System.Collections.Generic;
using System.Text;
using Transaction.Domain.Enum;

namespace Transaction.Domain.AggreagatesModels.Aggregate
{
    public class TransactionResponse
    {
        public string UniqId { get; set; }
        public enErrorCode ErrorCode { get; set; }
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
       
    }
}
