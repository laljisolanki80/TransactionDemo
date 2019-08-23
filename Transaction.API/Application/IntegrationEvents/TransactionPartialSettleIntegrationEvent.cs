using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Transaction.API.Application.IntegrationEvents
{
    public class TransactionPartialSettleIntegrationEvent
    {
        public string message { get; }
        public TransactionPartialSettleIntegrationEvent()
        {
            message = "your Transaction PartialSettle";

        }
    }
}
