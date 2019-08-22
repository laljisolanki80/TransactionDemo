using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;

namespace EventBusRabbitMQ
{
    //added for checking connection of RabbitMQ
    //Added by Lalji 06:05PM 10/08/2019
    public class DefaultRabbitMQPersistentConnection : IRabbitMQPersistentConnection
    {
        private IConnectionFactory _connectionFactory;
        IConnection _connection;
        bool _disposed;

        public bool IsConnected
        {
            get
            {
                return _connection != null && _connection.IsOpen;
            }
        }

        public DefaultRabbitMQPersistentConnection(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        public bool TryConnect()
        {
            _connection = _connectionFactory
                          .CreateConnection();

            if (IsConnected)
            {
                return true;
            }

            return false;
        }

        public IModel CreateModel()
        {
            if (!IsConnected)
            {
                throw new InvalidOperationException("No RabbitMQ connections are available to perform this action");
            }

            return _connection.CreateModel();
        }

        public void Dispose()
        {
            if (_disposed) return;

            _disposed = true;

            try
            {
                _connection.Dispose();
            }
            catch (IOException ex)
            {
                throw new IOException("RabbitMQ connection can not be disposed");
            }
        }
    }
}
