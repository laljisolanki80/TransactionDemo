using System;
using System.Collections.Generic;
using System.Text;

namespace EventBusRabbitMQ
{
   public interface IRabbitMQOperation
    {
        string SendMessage(string message);
    }
}