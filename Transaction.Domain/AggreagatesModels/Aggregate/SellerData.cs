using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Transaction.Domain.DomainEvents;
using Transaction.Domain.Enum;

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
        //public enErrorCode ErrorCode { get; set; }
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
            TransactionStatus = TransactionStatus.PartialSettle;
            AddDomainEvent(new TransactionPartialSettleDomainEvent(TransactionStatus));
        }
        public void StatusChangeToOnHoldStatus()
        {
            TransactionStatus = TransactionStatus.Hold;

            AddDomainEvent(new TransactionOnHoldDomainEvent(TransactionStatus));
        }
        public void StatusChangeToFailedStatus()
        {
            TransactionStatus = TransactionStatus.OperatorFail;
            AddDomainEvent(new TransactionFailedDomainEvent(TransactionStatus));
        }
        public void StatusChangeToCancleStatus() //by lalji 23/08/2019
        {
            TransactionStatus = TransactionStatus;//Transaction.cancel for cancel transaction status by lalji
            AddDomainEvent(new TransactionCancelDomainEvent(TransactionStatus));
        }

        public void InsertDateAndTime()
        {
            InsertTime = DateTime.Now;
        }
    }
}
