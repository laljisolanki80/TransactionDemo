using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Transaction.API.Application.IntegrationEvents
{
    public class TransactionFailedIntegrationEvent
    {
        public string message { get; }
        public TransactionFailedIntegrationEvent()
        {
            message = "OOps...your Transaction Failed";

        }
    }
}
