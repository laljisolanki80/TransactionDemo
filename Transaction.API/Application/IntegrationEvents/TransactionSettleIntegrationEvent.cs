using EventBusRabbitMQ.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Transaction.API.Application.IntegrationEvents
{
    public class TransactionSettleIntegrationEvent: IntegrationEvent
    {
        public string message { get; }
        public TransactionSettleIntegrationEvent()
        {
            message = "your transaction settle successfully";
        }
    }
}
