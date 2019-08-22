using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transaction.API.Application.Events.RabbitMQCommunication;

namespace Transaction.API.Application.Events
{
    public class TransactionOnHoldEvent
    {
        private RabbitMQSendMessage rabbitMQSendMessage;
        public TransactionOnHoldEvent(RabbitMQSendMessage rabbitMQSendMessage)
        {
            this.rabbitMQSendMessage = rabbitMQSendMessage;
        }


        readonly string message = "your transaction on hold";
        

        public void SendMessage()
        {
            rabbitMQSendMessage.SendMessage(message);
        }
    }
}
