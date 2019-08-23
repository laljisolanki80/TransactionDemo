using EventBusRabbitMQ.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transaction.API.Application.Events.RabbitMQCommunication;
using Transaction.API.Controllers;

namespace Transaction.API.Application.Events
{
    public class TransactionCancelEvent
    {
        private RabbitMQEventBus rabbitMQEventBus;

        public TransactionCancelEvent(RabbitMQEventBus rabbitMQEventBus)
        {
            this.rabbitMQEventBus = rabbitMQEventBus;
        }

        readonly string message = "your transaction has been cancel successfully";

        public void SendMessage()
        {
            rabbitMQSendMessage.SendMessage(message);
        }
    }
}
