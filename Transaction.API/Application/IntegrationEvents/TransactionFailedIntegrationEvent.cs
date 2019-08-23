using EventBusRabbitMQ.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Transaction.API.Application.IntegrationEvents
{
    public class TransactionFailedIntegrationEvent: IntegrationEvent
    {
        public string message { get; }
        public TransactionFailedIntegrationEvent()
        {
            message = "OOps...your Transaction Failed";

        }
    }
}
