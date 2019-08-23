using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Transaction.Domain.DomainEvents;

namespace Transaction.Domain.AggreagatesModels.Aggregate
{
    public class SellerData:Entity
    {
        [Key]
        public Guid SellerId { get; private set; }
        public decimal SellPrice { get; set; } 
        public decimal SellQuantity { get; set; }
        public decimal SettledQuantity { get; set; }
        public decimal RemainingQuantity { get; set; }
        public DateTime InsertTime { get; set; }
        public TransactionStatus TransactionStatus { get; set; }
        public SellerData()
        {

        }
        public SellerData(decimal sellPrice, decimal sellQuantity)
        {
            SellerId = Guid.NewGuid();
            SellPrice = sellPrice;
            SellQuantity = sellQuantity;
            RemainingQuantity = sellQuantity;
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
