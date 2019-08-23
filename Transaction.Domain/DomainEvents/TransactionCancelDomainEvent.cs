

using MediatR;
using Transaction.Domain.AggreagatesModels.Aggregate;

namespace Transaction.Domain.DomainEvents
{
   public class TransactionCancelDomainEvent: INotification
    {
        public TransactionStatus TransactionStatus { get;}
        public TransactionCancelDomainEvent(TransactionStatus transactionStatus)
        {
            TransactionStatus = transactionStatus;
        }
    }
}
