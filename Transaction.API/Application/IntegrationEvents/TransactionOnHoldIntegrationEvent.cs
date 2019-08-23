using EventBusRabbitMQ.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Transaction.API.Application.IntegrationEvents
{
    public class TransactionOnHoldIntegrationEvent: IntegrationEvent
    {
        public string message { get; }
        public TransactionOnHoldIntegrationEvent()
        {
            message = "your transaction on hold";

        }
    }
}
