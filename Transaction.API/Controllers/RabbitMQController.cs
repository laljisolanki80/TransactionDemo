﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;

namespace Transaction.API.Controllers
{
    [Produces("application/json")]
    [Route("api/RabbitMQ")]
    public class RabbitMQController : Controller
    {

        //this is testing controller for RabbitMQ by lalji 14/06/2016
        //private readonly string queueMessage;
        private readonly IModel consumerChannel;
        private const string ExchangeName = "BuyerTransaction_Exchange";
        private static IModel _model;
        private static ConnectionFactory _factory;
        private static IConnection _connection;
        //private static IModel _model;


        [HttpPost]
        [Route("Send")]
            public void SendMessage(string message)
            {
                     CreateConnection();
                var channel = consumerChannel;
                // channel.QueueDeclare(message, false, false, false, null);
                //channel.BasicPublish(string.Empty, null, null,Encoding.UTF8.GetBytes(message));
                _model.BasicPublish(ExchangeName, "", null, Encoding.UTF8.GetBytes(message));
                _model.ExchangeDeclare(ExchangeName, "fanout", false);

             }

        public static void CreateConnection()
        {
            _factory = new ConnectionFactory { HostName = "localhost", UserName = "guest", Password = "guest" };
            _connection = _factory.CreateConnection();
            _model = _connection.CreateModel();
            _model.ExchangeDeclare(ExchangeName, "fanout", false);
        }

    }






















    public class Message
    {
        private static ConnectionFactory _factory;
        private static IConnection _connection;
        private static IModel _model;
        private const string QueueName = "BuyerTransaction_Exchange";
        private const string AllQueueName = "AllPublish_Exchange";
        public static void send(string queueMessage)
        {
            queueMessage = "hello from testing";

            _model.BasicPublish(QueueName, "", null, Encoding.UTF8.GetBytes(queueMessage));


        }

        public void createConnection()
        {
            var channel=_connection.CreateModel();
            channel.ExchangeDeclare(QueueName, "total");
            channel.QueueDeclare(AllQueueName, true, true, false, null);
            channel.QueueBind(AllQueueName, QueueName,"Transaction");
            _factory = new ConnectionFactory { HostName = "localhost", UserName = "guest", Password = "guest" };
            _connection = _factory.CreateConnection();
          _model.ExchangeDeclare(QueueName, "fanout", false);
        }
    }
}