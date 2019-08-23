using EventBusRabbitMQ.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Transaction.API.Application.IntegrationEvents;
using Transaction.Domain.DomainEvents;

namespace Transaction.API.Application.DomainEventHandlers
{
    public class TransactionFailedDomainEventHandler : INotificationHandler<TransactionFailedDomainEvent>
    {
        private readonly IEventBus _eventBus;
        public TransactionFailedDomainEventHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }
        public async Task Handle(TransactionFailedDomainEvent notification, CancellationToken cancellationToken)
        {
            _eventBus.Publish(new TransactionFailedIntegrationEvent(), "TransactionFailedDomainEvent", "", "direct");
        }
    }
}
