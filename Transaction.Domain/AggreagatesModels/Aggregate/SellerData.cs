using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Transaction.Domain.AggreagatesModels.Aggregate
{
    public class SellerData
    {
        [Key]
        public Guid SellerId { get; private set; }
        public decimal SellPrice { get; set; } 
        public decimal SellQuantity { get; set; }
        public decimal SettledQuantity { get; set; }
        public decimal RemainingQuantity { get; set; }
        public DateTime InsertTime { get; set; }
        public TransactionStatus TransactionStatus { get; set; }
       
        public SellerData(decimal sellPrice, decimal sellQuantity,decimal settledQuantity,
            decimal remainingQuantity)
        {
            SellerId =Guid.NewGuid();
            SellPrice = sellPrice;
            SellQuantity = sellQuantity;
            SettledQuantity = settledQuantity;
            RemainingQuantity = remainingQuantity;
            InsertTime =DateTime.Now;
            TransactionStatus=TransactionStatus.Hold;
        }
    }
}
