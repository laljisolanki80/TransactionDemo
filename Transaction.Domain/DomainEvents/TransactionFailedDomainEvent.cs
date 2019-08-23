

using MediatR;
using Transaction.Domain.AggreagatesModels.Aggregate;

namespace Transaction.Domain.DomainEvents
{
   public class TransactionFailedDomainEvent: INotification
    {
        public TransactionStatus TransactionStatus { get; }
        public TransactionFailedDomainEvent(TransactionStatus transactionStatus)
        {
            TransactionStatus = transactionStatus;
        }
    }
}
