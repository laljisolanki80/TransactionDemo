using System;
using System.Collections.Generic;
using System.Text;

namespace Transaction.Domain.AggreagatesModels.Aggregate
{
   public class Transaction
    {
        //public TransactionStatus transactionStatus;
        public Guid guid{ get; set; }
        public int? _transactionStatusId;

        public void SetCancellledStatus()
        {
            
        }
    }
}
