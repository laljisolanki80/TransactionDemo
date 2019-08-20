using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventBusRabbitMQ;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Transaction.API.Controllers
{
    [Produces("application/json")]
    [Route("api/RabbitMQ")]
    public class RabbitMQController : Controller
    {
        //this is testing controller for RabbitMQ by lalji 14/06/2016

        private readonly IRabbitMQPersistentConnection persistentConnection;
        private const string ExchangeName = "BuyerTransaction_ExchangeTEST";
        private static IModel _model;
        private static ConnectionFactory _factory;
        private static RabbitMQ.Client.IConnection _connection;
        private string TestRabbit;
        private readonly IModel Channel;

        //private IModel consumerChannel;

        //private PersistentConnection _persistentConnection;

        //private static IModel _model;

        public RabbitMQController(IRabbitMQPersistentConnection persistentConnection)
        {
            this.persistentConnection = persistentConnection;
        }
        [HttpPost]
        [Route("Send")]
        public IActionResult SendMessage(string message)
        {
            CreateConnection();
            //var channel = consumerChannel;
            // channel.QueueDeclare(message, false, false, false, null);
            //channel.BasicPublish(string.Empty, null, null,Encoding.UTF8.GetBytes(message));
            _model.BasicPublish(ExchangeName, "", null, Encoding.UTF8.GetBytes(message));
            _model.ExchangeDeclare(ExchangeName, "fanout", false);

     
            return Ok();

        }
        public static void CreateConnection()
        {
            _factory = new ConnectionFactory { HostName = "localhost", UserName = "guest", Password = "guest" };
            _connection = _factory.CreateConnection();
            
            _model = _connection.CreateModel();
            _model.ExchangeDeclare(ExchangeName, "fanout", false);
        }

        

        [HttpGet]
        [Route("Get")]
        public IActionResult ReceiveMessage()
        {
            //var message = "";
            CreateConsumerChannel();
            return Ok();
        }


        public IModel CreateConsumerChannel()
        {
          
            if (!persistentConnection.IsConnected)
            {
                persistentConnection.TryConnect();
            }
            var channel = persistentConnection.CreateModel();

            channel.ExchangeDeclare(ExchangeName, type: "fanout", false);

            channel.QueueDeclare(queue: "TestRabbit",
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
            

            var consumer = new EventingBasicConsumer(channel);
            channel.BasicConsume("TestRabbit", false, consumer);
           // channel.ToString();

            consumer.Received += (model, ea) =>
            {
                //var eventName = ea.RoutingKey;
                var message = Encoding.UTF8.GetString(ea.Body);
                
            };
            return channel;
        }

    }
}
