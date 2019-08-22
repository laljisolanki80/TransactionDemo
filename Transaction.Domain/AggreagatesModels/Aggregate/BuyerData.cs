using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Transaction.Domain.AggreagatesModels.Aggregate
{
    public class BuyerData
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
    }
}
