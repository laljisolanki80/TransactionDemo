using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Transaction.API.Application.Model
{
    public class Customer
    {
        public string Name { get; set; }
        public int AccountId { get; set; }
        public int Amount { get; set; }

    }
}
