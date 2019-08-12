using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Transaction.Domain.AggreagatesModels.BuyerAggregate
{
    [Table("BuyerInformartion", Schema = "dbo")]
    public class BuyerInformartion
    {
        [Key]
        public int BuyerId { get; set; }

        [Display(Name = "Price(INR)")]
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
        public decimal Total { get; set; }
        public int TransationStatus { get; set; }
        [Timestamp]
        public byte[] TimeStamps { get; set; }
    }
}
