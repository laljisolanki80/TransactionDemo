using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks;
using Transaction.API.Application.Validation;
using System.Transactions;

namespace Transaction.API.Application.Models
{
    public class SellerDataModel
    {
       
        [Required]
        [GreaterThanZero(ErrorMessage = "Only positive number of Price allowed.")]
        [Column(TypeName = "decimal(28,18)")]
        [Display(Name = "Price(INR)")]
        public decimal SellPrice { get; set; }
        [Required]
        [GreaterThanZero(ErrorMessage = "Only positive number of Quantity allowed.")]
        public decimal SellQuantity { get; set; }
        public decimal SettledQuantity { get; set; }
        public decimal RemainingQuantity { get; set; }
        public TransactionStatus TransactionStatus { get; set; }
        public DateTime InsertTime { get; set; }
    }
}
