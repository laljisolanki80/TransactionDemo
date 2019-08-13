using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Transaction.API.Application.Queries
{
    public class Buyeritem
    {
        public decimal price { get; set; }
        public decimal quantity { get; set; }
    }
    public class Buyer
    {
        public List<Buyeritem> buyeritems { get; set; }
        public decimal total { get; set; }
    }
}
