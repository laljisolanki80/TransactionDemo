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
    public class TransactionOnHoldDomainEventHandler : INotificationHandler<TransactionOnHoldDomainEvent>
    {
        private readonly IEventBus _eventBus;
        public TransactionOnHoldDomainEventHandler(IEventBus eventBus)
        {
            _eventBus=eventBus;
        }
        public async Task Handle(TransactionOnHoldDomainEvent notification, CancellationToken cancellationToken)
        {
            _eventBus.Publish(new TransactionOnHoldIntegrationEvent(), "TransactionOnHoldDomainEvent", "", "direct");
        }
    }
}
