using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace EventBusRabbitMQ
{
   public class RabbitMQOperations:IRabbitMQOperation
    {
        private readonly IRabbitMQPersistentConnection persistentConnection;
        private readonly string queueName;
        private IModel consumerChannel;
        private IModel _model;

        //message pass when transaction initialize
        //pass message in queue

        public RabbitMQOperations(IRabbitMQPersistentConnection persistentConnection,string queueName = null)
        {
            this.persistentConnection = persistentConnection;
            this.queueName = queueName;
            this.consumerChannel = CreateConsumerChannel();
        }

        public string ExchangeName { get; private set; }

        public string SendMessage()
        {
            throw new NotImplementedException();
            //var channel = consumerChannel;
          //  _model.BasicPublish(ExchangeName, "", null, message.serilize());
          
            //channel.QueueDeclare(Queue:"msgkey",
            //    durability)

        }

        private IModel CreateConsumerChannel()
        {
            //Check rabbitMQ connection By Lalji 13/08/2019
            if (!persistentConnection.IsConnected)
            {
                persistentConnection.TryConnect();
            }

            var channel = persistentConnection.CreateModel();
            return channel;
        }
    }
}
