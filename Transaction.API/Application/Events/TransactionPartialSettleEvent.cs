using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transaction.API.Application.Events.RabbitMQCommunication;

namespace Transaction.API.Application.Events
{
    public class TransactionPartialSettleEvent
    {
        private RabbitMQSendMessage rabbitMQSendMessage;

        public TransactionPartialSettleEvent(RabbitMQSendMessage rabbitMQSendMessage)
        {
            this.rabbitMQSendMessage = rabbitMQSendMessage;
        }

        readonly string message = "your Transaction PartialSettle";

        public void SendMessage()
        {
            rabbitMQSendMessage.SendMessage(message);
        }
    }
}
