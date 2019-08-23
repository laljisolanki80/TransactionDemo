
using MediatR;
using Transaction.Domain.AggreagatesModels.Aggregate;

namespace Transaction.Domain.DomainEvents
{
   public class TransactionOnHoldDomainEvent: INotification
    {
        public TransactionStatus TransactionStatus { get; }
        public TransactionOnHoldDomainEvent(TransactionStatus transactionStatus)
        {
            TransactionStatus = transactionStatus;
        }
    }
}
