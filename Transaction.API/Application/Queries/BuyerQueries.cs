using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Transaction.API.Application.Queries
{
    public class BuyerQueries : IBuyerQueries
    {
        private string queriesConnectionString;

        public BuyerQueries(string queriesConnectionString)
        {
            this.queriesConnectionString = queriesConnectionString;
        }
    }
}
