using System;
using System.Collections.Generic;
using System.Text;
using Transaction.Domain.SeedWork;

namespace Transaction.Domain.AggreagatesModels.BuyerAggregate
{
    public class BuyerTransactionStatus:Enumeration
    {
       
        public static BuyerTransactionStatus success = new BuyerTransactionStatus(1, nameof(success).ToLowerInvariant());
        public static BuyerTransactionStatus PartialHold = new BuyerTransactionStatus(2, nameof(PartialHold).ToLowerInvariant());
        public static BuyerTransactionStatus SystemFailed = new BuyerTransactionStatus(3, nameof(SystemFailed).ToLowerInvariant());
        public static BuyerTransactionStatus Hold = new BuyerTransactionStatus(4, nameof(Hold).ToLowerInvariant());
        public BuyerTransactionStatus()
        {

        }
        public BuyerTransactionStatus(int id, string name)
        {
        }
    }
}
