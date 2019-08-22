using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Transaction.Domain.AggreagatesModels.Aggregate
{
    public class Ledger:Entity
    {
        [StringLength(50)]
        public string DisplayId { get; set; }
        [StringLength(50)]
        public string BuyerId { get; set; }
        public double BuyerPrice { get; set; }
        [StringLength(50)]
        public string SellerId { get; set; }
        public double SellerPrice { get; set; }
        public long SellerQuantity { get; set; }
        public DateTime ProcessTime { get; set; }
        public TransactionStatus TransactionStatus { get; set; }
    }
}
