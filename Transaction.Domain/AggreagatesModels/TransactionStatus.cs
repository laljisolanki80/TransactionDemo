using System;
using System.Collections.Generic;
using System.Text;

namespace Transaction.Domain.AggreagatesModels
{
    class TransactionStatus
    {
        public static TransactionStatus success = new TransactionStatus(1, nameof(success).ToLowerInvariant());
        public static TransactionStatus fail = new TransactionStatus(1, nameof(fail).ToLowerInvariant());
        public static TransactionStatus cancel = new TransactionStatus(1, nameof(cancel).ToLowerInvariant());
        public TransactionStatus(int accountId,string success)
            {
                
            }
   
    }
}
