using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transaction.API.Application.Events.RabbitMQCommunication
{
    public class RabbitMQSendMessage
    {
        private const string ExchangeName = "BuyerTransaction_ExchangeTEST";
        private static IModel _model;
        private static ConnectionFactory _factory;
        private static RabbitMQ.Client.IConnection _connection;

        public void  SendMessage(string message)
        {
            CreateConnection();
            //string message="";
            _model.BasicPublish(ExchangeName, "", null, Encoding.UTF8.GetBytes(message));
            _model.ExchangeDeclare(ExchangeName, "fanout", true);

           
            //return message;
        }
        public static void CreateConnection()
        {
            _factory = new ConnectionFactory { HostName = "localhost", UserName = "guest", Password = "guest" };
            _connection = _factory.CreateConnection();

            _model = _connection.CreateModel();
            _model.ExchangeDeclare(ExchangeName, "fanout", true);
        }

    }
}
