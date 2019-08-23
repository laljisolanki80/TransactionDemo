using EventBusRabbitMQ.Events;
using System.Threading.Tasks;

namespace EventBusRabbitMQ.EventHandlers
{
    public interface IIntegrationEventHandler<in TIntegrationEvent> : IIntegrationEventHandler
        where TIntegrationEvent : IntegrationEvent
    {
        Task Handle(TIntegrationEvent @event);
    }

    public interface IIntegrationEventHandler
    {
    }
}
