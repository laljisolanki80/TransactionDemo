using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventBusRabbitMQ
{
    //interface added by Lalji 
    //for connection check :05:29PM 12/08/2019
   public interface IRabbitMQPersistentConnection:IDisposable
    {

        bool IsConnected { get; }

        bool TryConnect();

        IModel CreateModel();
    }
}
