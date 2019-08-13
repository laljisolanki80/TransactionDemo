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
        public int Id { get; set; }//changes for name 

        [Required]
        [Column(TypeName = "decimal(28,18)")]
        [Display(Name = "Price(INR)")] //Datatype should define
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
        public decimal Total { get; set; }
        public int TransationStatus { get; set; }
        [Timestamp]
        public byte[] TimeStamps { get; set; }
    }
}
