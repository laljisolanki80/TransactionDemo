using System;
using System.Collections.Generic;
using System.Text;

namespace Transaction.Domain.AggreagatesModels.BuyerAggregate
{
   public class BuyerStatus
    {
        public static BuyerStatus Submitted = new BuyerStatus(1, nameof(Submitted).ToLowerInvariant());
        public static BuyerStatus AwaitingValidation = new BuyerStatus(2, nameof(AwaitingValidation).ToLowerInvariant());
        public static BuyerStatus StockConfirmed = new BuyerStatus(3, nameof(StockConfirmed).ToLowerInvariant());
        public static BuyerStatus Paid = new BuyerStatus(4, nameof(Paid).ToLowerInvariant());
        public static BuyerStatus Cancelled = new BuyerStatus(5, nameof(Cancelled).ToLowerInvariant());
        private int id;
        private string name;

        protected BuyerStatus()
        {

        }
        public BuyerStatus(int id, string name)
        {
            this.id = id;
            this.name = name;       
        }

        
    }
}
