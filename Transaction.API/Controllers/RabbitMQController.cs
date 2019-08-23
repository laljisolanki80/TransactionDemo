using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventBusRabbitMQ;
using EventBusRabbitMQ.Interfaces;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Transaction.API.Application.Events;

namespace Transaction.API.Controllers
{
    [Produces("application/json")]
    [Route("api/RabbitMQ")]
    public class RabbitMQController : Controller
    {
        //this is testing controller for RabbitMQ by lalji 14/06/2016

        private readonly IRabbitMQPersistentConnection persistentConnection;
        private readonly TransactionCancelEvent transactionCancelEvent;
        private const string ExchangeName = "BuyerTransaction_ExchangeTEST";
        private static IModel _model;
        private static ConnectionFactory _factory;
        private static RabbitMQ.Client.IConnection _connection;
        private string TestRabbit;
        private IRabbitMQPersistentConnection mQPersistentConnection;
        private readonly IModel Channel;
        private readonly TransactionFailedEvent transactionFailedEvent;
        private readonly TransactionSettleEvent transactionSettleEvent;
        private readonly TransactionPartialSettleEvent transactionPartialSettleEvent;
        private readonly TransactionOnHoldEvent transactionOnHoldEvent;

        public RabbitMQController(IRabbitMQPersistentConnection mQPersistentConnection)
        {
            this.mQPersistentConnection = mQPersistentConnection;

            this.transactionCancelEvent = new TransactionCancelEvent(new Application.Events.RabbitMQCommunication.RabbitMQSendMessage());
            this.transactionFailedEvent = new TransactionFailedEvent(new Application.Events.RabbitMQCommunication.RabbitMQSendMessage());
            this.transactionSettleEvent = new TransactionSettleEvent(new Application.Events.RabbitMQCommunication.RabbitMQSendMessage());
            this.transactionPartialSettleEvent = new TransactionPartialSettleEvent(new Application.Events.RabbitMQCommunication.RabbitMQSendMessage());
            this.transactionOnHoldEvent = new TransactionOnHoldEvent(new Application.Events.RabbitMQCommunication.RabbitMQSendMessage());
        }

        [HttpPost]
        [Route("Send")]
        public IActionResult SendMessage(string message)
        {
            //CreateConnection();
            //var channel = consumerChannel;
            // channel.QueueDeclare(message, false, false, false, null);
            //channel.BasicPublish(string.Empty, null, null,Encoding.UTF8.GetBytes(message));

            _model.BasicPublish(ExchangeName, "", null, Encoding.UTF8.GetBytes(message));
            _model.ExchangeDeclare(ExchangeName, "fanout", true);


            return Ok();

        }
        //public static void CreateConnection()
        //{
        //    _factory = new ConnectionFactory { HostName = "localhost", UserName = "guest", Password = "guest" };
        //    _connection = _factory.CreateConnection();

        //    _model = _connection.CreateModel();
        //    _model.ExchangeDeclare(ExchangeName, "fanout", true);
        //}



        [HttpGet]
        [Route("Get")]
        public IActionResult ReceiveMessage()
        {
            //CreateConnection();
            var message = CreateConsumerChannel();
            return Ok(message);
        }


        public string CreateConsumerChannel()
        {
            var message = "";

            //CreateConnection();

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

          //for testing event message of transaction status By lalji

        [HttpGet]
        [Route("hold")]
        public IActionResult Test()
        {
            transactionOnHoldEvent.SendMessage();
            return Ok();
        }

        [HttpGet]
        [Route("cancel")]
        public IActionResult Test1()
        {
            transactionCancelEvent.SendMessage();
            return Ok();
        }
        [HttpGet]
        [Route("failed")]
        public IActionResult Test2()
        {
            transactionFailedEvent.SendMessage();
            return Ok();
        }

        [HttpGet]
        [Route("settle")]
        public IActionResult Test3()
        {
            transactionSettleEvent.SendMessage();
            return Ok();
        }

        [HttpGet]
        [Route("partial")]
        public IActionResult Test4()
        {
            transactionPartialSettleEvent.SendMessage();
            return Ok();
        }
    }
}
