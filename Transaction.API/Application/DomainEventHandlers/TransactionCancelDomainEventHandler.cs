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
    public class TransactionCancelDomainEventHandler : INotificationHandler<TransactionCancelDomainEvent>
    {
        private readonly IEventBus _eventBus;

        public TransactionCancelDomainEventHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }
        public async Task Handle(TransactionCancelDomainEvent notification, CancellationToken cancellationToken)
        {
            _eventBus.Publish(new TransactionCancelIntegrationEvent(), "TransactionCancelDomainEvent", "", "direct");
        }
    }
}
