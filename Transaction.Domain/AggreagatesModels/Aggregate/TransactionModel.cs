using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Transaction.Domain.Validation;

namespace Transaction.Domain.AggreagatesModels.Aggregate
{
    public class TransactionModel
    {
        [Required]
        [GreaterThanZero(ErrorMessage = "Only positive number of Price allowed.")]
        [Column(TypeName = "decimal(28,18)")]
        [Display(Name = "Price(INR)")]
        public decimal Price { get; set; }
        [Required]
        [GreaterThanZero(ErrorMessage = "Only positive number of Quantity allowed.")]
        public decimal Quantity { get; set; }
    }
}
