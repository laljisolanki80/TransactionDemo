using EventBusRabbitMQ.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Transaction.API.Application.IntegrationEvents
{
    public class TransactionCancelIntegrationEvent: IntegrationEvent
    {
        public string message { get; }
        public TransactionCancelIntegrationEvent()
        {
            message = "your transaction has been cancel successfully";
        }
    }
}
