using System;
using System.ComponentModel.DataAnnotations;
using Transaction.Domain.DomainEvents;

namespace Transaction.Domain.AggreagatesModels.Aggregate
{
    public class BuyerData:Entity
    {
        [Key]
        public Guid BuyId { get; set; }
        public decimal BuyPrice { get; set; }
        public decimal BuyQuantity { get; set; }
        public decimal SettledQuantity { get; set; }
        public decimal RemainingQuantity { get; set; }
        public DateTime InsertTime { get; set; }
        public TransactionStatus TransactionStatus { get; set; }
        public BuyerData()
        {

        }
        public BuyerData(decimal buyPrice,decimal buyQuantity)
        {
            BuyId = Guid.NewGuid();
            BuyPrice = buyPrice;
            BuyQuantity = buyQuantity;
            RemainingQuantity = buyQuantity;
            InsertTime = DateTime.Now;
            TransactionStatus = TransactionStatus.Hold;
        }
        public void StatusChangeToSettleStatus()
        {
            TransactionStatus = TransactionStatus.Success;
            AddDomainEvent(new TransactionSettleDomainEvent(TransactionStatus));
        }
        public void StatusChangeToPartialSettleStatus()
        {
            TransactionStatus = TransactionStatus.Pending;
            AddDomainEvent(new TransactionPartialSettleDomainEvent(TransactionStatus));
        }
        public void StatusChangeToOnHoldStatus()
        {
            TransactionStatus = TransactionStatus.Hold;

            AddDomainEvent(new TransactionOnHoldDomainEvent(TransactionStatus));
        }
        public void StatusChangeToFailedStatus()
        {
            TransactionStatus = TransactionStatus.SystemFail;
            AddDomainEvent(new TransactionFailedDomainEvent(TransactionStatus));
        }
        public void StatusChangeToCancleStatus()
        {
            TransactionStatus = TransactionStatus.Refunded;
            AddDomainEvent(new TransactionCancelDomainEvent(TransactionStatus));
        }
    }
}
