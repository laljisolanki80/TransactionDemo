using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transaction.API.Application.Events.RabbitMQCommunication;

namespace Transaction.API.Application.Events
{
    public class TransactionSettleEvent
    {
        private RabbitMQSendMessage rabbitMQSendMessage;

        public TransactionSettleEvent(RabbitMQSendMessage rabbitMQSendMessage)
        {
            this.rabbitMQSendMessage = rabbitMQSendMessage;
        }

        readonly string message = "your transaction settle successfully";

        public void SendMessage()
        {
            rabbitMQSendMessage.SendMessage(message);
        }
    }
}
