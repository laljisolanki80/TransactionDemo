using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transaction.API.Application.Events.RabbitMQCommunication;

namespace Transaction.API.Application.Events
{
    public class TransactionFailedEvent
    {
        private RabbitMQSendMessage rabbitMQSendMessage;

        public TransactionFailedEvent(RabbitMQSendMessage rabbitMQSendMessage)
        {
            this.rabbitMQSendMessage = rabbitMQSendMessage;
        }

        string message = "OOps...your Transaction Failed";

        public void SendMessage()
        {
            rabbitMQSendMessage.SendMessage(message);
        }
    }
}
