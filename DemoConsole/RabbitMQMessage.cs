using RabbitMQ.Client;
using RabbitMQ.Client.MessagePatterns;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoConsole
{
   public class RabbitMQMessage
    {
        private static ConnectionFactory _factory;
        private static IConnection _connection;
        private static IModel _model;

       // private const string ExchangeName = "PublishSubscribe_Exchange";

        private const string ExchangeName = "Topic_Exchange";
        private const string AllQueueName = "AllTopic_Queue";

        public void CreateConnection()
        {
            _factory = new ConnectionFactory { HostName = "localhost", UserName = "guest", Password = "guest" };
        }

        public void Close()
        {
            _connection.Close();
        }

        public void ProcessMessages()
        {
            using (_connection = _factory.CreateConnection())
            {
                using (var channel = _connection.CreateModel())
                {
                    Console.WriteLine("Listening for Transaction");
                    Console.WriteLine("------------------------------");
                    Console.WriteLine();

                    channel.ExchangeDeclare(ExchangeName, "topic");
                    channel.QueueDeclare(AllQueueName, true, false, false, null);
                    channel.QueueBind(AllQueueName, ExchangeName, "This is demo message in queue");

                    channel.BasicQos(0, 10, false);
                    Subscription subscription = new Subscription(channel, AllQueueName, false);
                }
            }

        }
        private static void SendMessage(string queueMessage)
        {
            queueMessage = "hello from testing";
            _model.BasicPublish(ExchangeName, "", null, Encoding.UTF8.GetBytes(queueMessage));

        }

    }
}

