using System;

namespace DemoConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            RabbitMQMessage client = new RabbitMQMessage();
            client.CreateConnection();
            client.ProcessMessages();

        }
    }
}
