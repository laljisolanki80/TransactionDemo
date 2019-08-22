using System;
using System.Collections.Generic;
using System.Text;

namespace Transaction.Domain.AggreagatesModels.Aggregate
{
    public class TransactionResponse
    {
        public string UniqId { get; set; }
        public string ErrorCode { get; set; }
        public string StatusCode { get; set; }
        public string StatusMessage { get; set; }
       
    }
}
