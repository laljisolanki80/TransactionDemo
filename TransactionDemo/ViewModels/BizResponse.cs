using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransactionDemo.ViewModels
{
    public class BizResponse
    {
        public enErrorCode ErrorCode { get; set; }
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
    }
}
