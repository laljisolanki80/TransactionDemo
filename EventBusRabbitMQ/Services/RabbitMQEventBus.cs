﻿using EventBusRabbitMQ.Events;
using EventBusRabbitMQ.Interfaces;
using NLog;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;
using Autofac;
using EventBusRabbitMQ.EventHandlers;
using EventBusRabbitMQ.Extentions;
using RabbitMQ.Client.Events;
using System.Threading.Tasks;

namespace EventBusRabbitMQ.Services
{

    public class RabbitMQEventBus : IEventBus, IDisposable
    {
        private readonly IRabbitMQPersistentConnection _persistentConnection;
        private readonly Logger _logger;
        private readonly IEventBusSubscriptionsManager _subsManager;
        private readonly ILifetimeScope _autofac;

        private string AUTOFAC_SCOPE_NAME;
        private IModel _consumerChannel;

        public RabbitMQEventBus(IRabbitMQPersistentConnection persistentConnection, Logger logger,
            IEventBusSubscriptionsManager subsManager=null,ILifetimeScope autofac=null)
        {
            _persistentConnection = persistentConnection ?? throw new ArgumentNullException(nameof(persistentConnection));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _subsManager = subsManager;
            _autofac = autofac;

            if(_subsManager!=null)
            {
                _subsManager.OnEventRemoved += SubsManager_OnEventRemoved;
            }
        }

        private void SubsManager_OnEventRemoved(object sender, string eventName)
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }
            else
            {

                using (var channel = _persistentConnection.CreateModel())
                {
                    var containsKey = _subsManager.HasSubscriptionsForEvent(eventName);
                    if (containsKey)
                    {
                        var subscriptions = _subsManager.GetHandlersForEvent(eventName);
                        foreach (var subscription in subscriptions)
                        {

                            channel.QueueUnbind(queue: subscription.QueueName,
                            exchange: subscription.BrokerName,
                            routingKey: subscription.RoutingKey);

                            if (_subsManager.IsEmpty)
                            {
                                subscription.QueueName = string.Empty;
                                _consumerChannel.Close();
                            }
                        }

                    }
                }
            }
        }

        public void Publish(IntegrationEvent @event, string brokerName, string routingKey, string typeOfExchange)
        {
            try
            {
                if (!_persistentConnection.IsConnected)
                {
                    _persistentConnection.TryConnect();
                    _logger.Info("\nDate:" + DateTime.UtcNow + "\nMethodName:" + "TryConnect" + ", Controllername: " + "Publish" + "\nAccessToken: " + "\nRequest: " + "RabbitMq Sucessfully Connected...." + "\n===================================================================================================================");
                }
                else
                {
                    using (var channel = _persistentConnection.CreateModel())
                    {
                        var eventName = @event.GetType().Name;

                        channel.ExchangeDeclare(exchange: brokerName,
                                            type: typeOfExchange, true);

                        var message = JsonConvert.SerializeObject(@event);
                        var body = Encoding.UTF8.GetBytes(message);


                        var properties = channel.CreateBasicProperties();
                        properties.DeliveryMode = 2; // persistent

                        channel.BasicPublish(exchange: brokerName,
                                         routingKey: routingKey,
                                         mandatory: true,
                                         basicProperties: properties,
                                         body: body);
                        _logger.Info("\nDate:" + DateTime.UtcNow + "\nMethodName:" + "BasicPublish" + ", Controllername: " + "Publish" + "\nAccessToken: " + "\nRequest: " + eventName + " Published..." + "\n===================================================================================================================");

                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("\nDate:" + TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time")) + "\nMethodName:" + "Publish" + ",Controllername: " + "RabitMQEventBus" + "\nError: " + ex.Message + "\nOtherInfo:" + "===================================================================================================================");
            }
           

        }

        public void Subscribe<T, TH>(string queueName, string brokerName, string routingKey, string TypeOfExchange)
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var eventName = _subsManager.GetEventKey<T>();
            DoInternalSubscription(eventName, queueName, brokerName, routingKey, TypeOfExchange);

            _logger.Info("Subscribing to event {EventName} with {EventHandler}", eventName, typeof(TH).GetGenericTypeName());

            _subsManager.AddSubscription<T, TH>(queueName, brokerName, routingKey);
        }

        private void DoInternalSubscription(string eventName, string queueName, string brokerName, string routingKey, string TypeOfExchange)
        {
            var containsKey = _subsManager.HasSubscriptionsForEvent(eventName);
            if (!containsKey)
            {
                if (!_persistentConnection.IsConnected)
                {
                    _persistentConnection.TryConnect();
                }

                using (var channel = _persistentConnection.CreateModel())
                {
                    CreateConsumerChannel(queueName, brokerName, routingKey, TypeOfExchange);
                }

            }
        }

        public void Unsubscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var eventName = _subsManager.GetEventKey<T>();

            _logger.Info("Unsubscribing from event {EventName}", eventName);

            _subsManager.RemoveSubscription<T, TH>();
        }

        public void Dispose()
        {
            if (_consumerChannel != null)
            {
                _consumerChannel.Dispose();
            }

            _subsManager.Clear();
        }

        private IModel CreateConsumerChannel(string queueName, string brokerName, string routingKey, string TypeOfExchange)
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }

            AUTOFAC_SCOPE_NAME = brokerName;
            var channel = _persistentConnection.CreateModel();

            channel.ExchangeDeclare(exchange: brokerName,
                                 type: TypeOfExchange, true);

            channel.QueueDeclare(queue: queueName,
                                            durable: true,
                                            exclusive: false,
                                            autoDelete: false,
                                            arguments: null);

            channel.QueueBind(queue: queueName,
                             exchange: brokerName,
                             routingKey: routingKey);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
            {
                var _eventName = ea.RoutingKey;
                var _message = Encoding.UTF8.GetString(ea.Body);

                await ProcessEvent(_eventName, _message);

                channel.BasicAck(ea.DeliveryTag, multiple: false);
            };

            channel.BasicConsume(queue: queueName,
                                 autoAck: false,
                                 consumer: consumer);

            channel.CallbackException += (sender, ea) =>
            {
                _consumerChannel.Dispose();
                _consumerChannel = CreateConsumerChannel(queueName, brokerName, routingKey, TypeOfExchange);
            };

            return channel;
        }

        private async Task ProcessEvent(string eventName, string message)
        {
            if (_subsManager.HasSubscriptionsForEvent(eventName))
            {
                using (var scope = _autofac.BeginLifetimeScope(AUTOFAC_SCOPE_NAME))
                {
                    var subscriptions = _subsManager.GetHandlersForEvent(eventName);
                    foreach (var subscription in subscriptions)
                    {
                        var handler = scope.ResolveOptional(subscription.HandlerType);
                        if (handler == null) continue;
                        var eventType = _subsManager.GetEventTypeByName(eventName);
                        var integrationEvent = JsonConvert.DeserializeObject(message, eventType);
                        var concreteType = typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);
                        await (Task)concreteType.GetMethod("Handle").Invoke(handler, new object[] { integrationEvent });
                    }
                }
            }
        }
    }
}
