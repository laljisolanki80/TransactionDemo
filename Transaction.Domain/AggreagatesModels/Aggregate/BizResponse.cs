using System;
using System.Collections.Generic;
using System.Text;
using Transaction.Domain.Enum;

namespace Transaction.Domain.AggreagatesModels.Aggregate
{
    public class BizResponse
    {
        public enErrorCode ErrorCode { get; set; }
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
        
    }
}
