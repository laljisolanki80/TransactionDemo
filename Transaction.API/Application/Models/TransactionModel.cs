using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Transaction.API.Application.Models
{
    public class TransactionModel
    {
        public Guid Id { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
    }
}
