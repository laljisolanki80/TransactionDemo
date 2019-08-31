using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TransactionDemo.ViewModels
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
