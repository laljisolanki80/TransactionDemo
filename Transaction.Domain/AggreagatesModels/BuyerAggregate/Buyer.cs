using System;
using System.Collections.Generic;
using System.Text;

namespace Transaction.Domain.AggreagatesModels.BuyerAggregate
{
    public class Buyer
    {
        private DateTime _buyDate;
        public String Street { get; private set; }
    }
}
