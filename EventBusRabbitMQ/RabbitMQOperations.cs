using RabbitMQ.Client;
using RabbitMQ.Client.Events;
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
        private const string ExchangeName = "BuyerTransaction_ExchangeFromOperation";

        //message pass when transaction initialize
        //pass message in queue

        public RabbitMQOperations(IRabbitMQPersistentConnection persistentConnection, string queueName = null)
        {
            this.persistentConnection = persistentConnection;
            this.queueName = queueName;
            this.consumerChannel = CreateConsumerChannel();
        }



        private IModel CreateConsumerChannel()
        {
            //check of rabbitmq connection and connect if connection is not exist -Sahil 12-08-2019
            if (!persistentConnection.IsConnected)
            {
                persistentConnection.TryConnect();
            }


            var channel = persistentConnection.CreateModel();
            channel.ExchangeDeclare(exchange: ExchangeName, type: "direct", true);

            channel.QueueDeclare(queue: queueName,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received +=  (model, ea) =>
            {
                var eventName = ea.RoutingKey;
                var message = Encoding.UTF8.GetString(ea.Body);

                //await ProcessEvent(eventName, message);

                channel.BasicAck(ea.DeliveryTag, multiple: false);
            };

            channel.BasicConsume(queue: queueName,
                                 autoAck: false,
                                 consumer: consumer);

            return channel;
        }


        //public IModel CreateConsumerChannel()
        //{
        //    //Check rabbitMQ connection By Lalji 13/08/2019
        //    if (!persistentConnection.IsConnected)
        //    {
        //        persistentConnection.TryConnect();
        //    }

        //    var channel = persistentConnection.CreateModel();

        //    channel.ExchangeDeclare(exchange: ExchangeName, type: "fanout", true);

        //    channel.QueueDeclare(queue: queueName,
        //                         durable: true,
        //                         exclusive: false,
        //                         autoDelete: false,
        //                         arguments: null);

        //    var consumer = new EventingBasicConsumer(channel);
        //    consumer.Received +=  (model, ea) =>
        //    {
        //        var eventName = ea.RoutingKey;
        //        var message = Encoding.UTF8.GetString(ea.Body);


        //        channel.BasicAck(ea.DeliveryTag, multiple: false);
        //    };

        //    channel.BasicConsume(queue: queueName,
        //                         autoAck: false,
        //                         consumer: consumer);

        //    return channel;
        //}

        public string SendMessage(String message)
        {
            var channel = consumerChannel;
            // channel.QueueDeclare(message, false, false, false, null);
            //channel.BasicPublish(string.Empty, null, null,Encoding.UTF8.GetBytes(message));
            _model.BasicPublish(ExchangeName, "", null, Encoding.UTF8.GetBytes(message));
            _model.ExchangeDeclare(ExchangeName, "fanout", false);

            return message;
        }


    }
}
