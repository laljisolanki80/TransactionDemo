using System;
using System.Collections.Generic;
using System.Text;

namespace EventBusRabbitMQ
{
   public interface IRabbitMQOperation
    {
        //messge
        string SendMessage(string message);
    }
}