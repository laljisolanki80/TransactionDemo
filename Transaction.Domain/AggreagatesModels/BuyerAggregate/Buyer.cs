using System;
using System.Collections.Generic;
using System.Text;

namespace Transaction.Domain.AggreagatesModels.BuyerAggregate
{
    public class Buyer
    { 
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
