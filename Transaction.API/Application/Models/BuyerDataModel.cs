using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Transaction.API.Application.Validation;

namespace Transaction.API.Application.Models
{
    public class BuyerDataModel
    {
        
        [Required]
        [GreaterThanZero(ErrorMessage = "Only positive number of Price allowed.")]
        [Column(TypeName = "decimal(28,18)")]
        [Display(Name = "Price(INR)")]
        public decimal BuyPrice { get; set; }
        [Required]
        [GreaterThanZero(ErrorMessage = "Only positive number of Quantity allowed.")]
        public decimal BuyQuantity { get; set; }
        public decimal SettledQuantity { get; set; }
        public decimal RemainingQuantity { get; set; }
        public TransactionStatus TransactionStatus { get; set; }
        public DateTime InsertTime { get; set; }
    
        

    }
}
