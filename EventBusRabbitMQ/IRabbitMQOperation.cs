using System;
using System.Collections.Generic;
using System.Text;

namespace EventBusRabbitMQ
{
   public interface IRabbitMQOperation
    {
        string SendMessage(string queue,string data);
        string SendMessage(string message, string data);
    }
}