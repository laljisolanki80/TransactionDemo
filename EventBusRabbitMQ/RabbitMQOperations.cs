using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace EventBusRabbitMQ
{
    public class RabbitMQOperations : IRabbitMQOperation
    {
        private readonly IRabbitMQPersistentConnection persistentConnection;
        private readonly string queueName;
        private readonly IModel consumerChannel;
        private IModel _model;
        private const string ExchangeName = "BuyerTransaction_Exchange";

        //message pass when transaction initialize
        //pass message in queue

        public RabbitMQOperations(IRabbitMQPersistentConnection persistentConnection, string queueName = null)
        {
            this.persistentConnection = persistentConnection;
            this.queueName = queueName;
            this.consumerChannel = CreateConsumerChannel();
        }

        public string SendMessage(String message)
        {
                    var channel = consumerChannel;
            // channel.QueueDeclare(message, false, false, false, null);
            //channel.BasicPublish(string.Empty, null, null,Encoding.UTF8.GetBytes(message));
            _model.BasicPublish(ExchangeName, "", null, Encoding.UTF8.GetBytes(message));
            _model.ExchangeDeclare(ExchangeName, "fanout", false);

            return message;
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
