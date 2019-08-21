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
            _model.ExchangeDeclare(ExchangeName, "fanout", true);

     
            return Ok();

        }
        public static void CreateConnection()
        {
            _factory = new ConnectionFactory { HostName = "localhost", UserName = "guest", Password = "guest" };
            _connection = _factory.CreateConnection();
            
            _model = _connection.CreateModel();
            _model.ExchangeDeclare(ExchangeName, "fanout", true);
        }

        

        [HttpGet]
        [Route("Get")]
        public IActionResult ReceiveMessage()
        {
            CreateConnection();
            var message = CreateConsumerChannel();
            return Ok(message);
        }


        public string CreateConsumerChannel()
        {
            var message = "";

            CreateConnection();

            using (var channel = _connection.CreateModel())
            {
                channel.ExchangeDeclare(ExchangeName, type: "fanout", true);

                channel.QueueDeclare(queue: ExchangeName,
                     durable: true,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

                channel.QueueBind(queue: ExchangeName, exchange: ExchangeName, routingKey: "", arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                channel.BasicConsume(ExchangeName, false, consumer);

                

                consumer.Received += (model, ea) =>
                {
                    //var eventName = ea.RoutingKey;
                    message += Encoding.UTF8.GetString(ea.Body);
                    channel.BasicAck(ea.DeliveryTag, multiple: false);
                };

                channel.QueuePurge(ExchangeName);
            }

            return message;
        }

    }
}
