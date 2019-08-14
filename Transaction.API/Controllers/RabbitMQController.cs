using System;
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
        private readonly string queueMessage;

        [HttpPost]
        [Route("Send")]
        public void send([FromBody] Message message)
        {

            message.createConnection();
          //  return message;
        }
    }

    public class Message
    {
        private static ConnectionFactory _factory;
        private static IConnection _connection;
        private static IModel _model;
        private const string QueueName = "Publish_Exchange";
        private const string AllQueueName = "AllPublish_Queue";
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