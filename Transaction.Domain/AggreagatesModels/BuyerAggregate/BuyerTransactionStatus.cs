using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Transaction.Domain.Exceptions;
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
            :base(id,name)
        {
        }
        public static IEnumerable<BuyerTransactionStatus> List() =>
            new[] { success,PartialHold,SystemFailed,Hold };
        public static BuyerTransactionStatus FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new TransactionDomainException($"Possible values for BuyerStatus: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }
        public static BuyerTransactionStatus From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new TransactionDomainException($"Possible values for BuyerStatus: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

    }
}
