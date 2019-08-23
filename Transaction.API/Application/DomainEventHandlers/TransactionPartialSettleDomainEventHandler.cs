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
    public class TransactionPartialSettleDomainEventHandler : INotificationHandler<TransactionPartialSettleDomainEvent>
    {
        private readonly IEventBus _eventBus;

        public TransactionPartialSettleDomainEventHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }
        public async Task Handle(TransactionPartialSettleDomainEvent notification, CancellationToken cancellationToken)
        {
            _eventBus.Publish(new TransactionPartialSettleIntegrationEvent(), "TransactionPartialSettleIntegrationEvent", "", "direct");
        }
    }
}
