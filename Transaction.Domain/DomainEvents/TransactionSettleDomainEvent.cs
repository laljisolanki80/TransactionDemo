
using MediatR;
using Transaction.Domain.AggreagatesModels.Aggregate;

namespace Transaction.Domain.DomainEvents
{
    public class TransactionSettleDomainEvent: INotification
    {
        public TransactionStatus TransactionStatus { get; }
        public TransactionSettleDomainEvent(TransactionStatus transactionStatus)
        {
            TransactionStatus = transactionStatus;
        }
    }
}
