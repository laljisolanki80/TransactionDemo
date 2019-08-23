
using MediatR;
using Transaction.Domain.AggreagatesModels.Aggregate;

namespace Transaction.Domain.DomainEvents
{
   public class TransactionPartialSettleDomainEvent: INotification
    {
        public TransactionStatus TransactionStatus { get; }
        public TransactionPartialSettleDomainEvent(TransactionStatus transactionStatus)
        {
            TransactionStatus = transactionStatus;
        }
    }
}
