using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Transaction.API.Application.IntegrationEvents;
using Transaction.Domain.AggreagatesModels.BuyerAggregate;
using Transaction.Domain.Events;

namespace Transaction.API.Application.DomainEventsHandler
{
    public class ValidateOrAddBuyerWhenBuyerStartedDomainEventHandler
        : INotificationHandler<BuyStartedDomainEvent>
    {
        private readonly IBuyerRepository _buyerRepository;
        private readonly IBuyerIntegrationEventService _buyerIntegrationEventService;
        public ValidateOrAddBuyerWhenBuyerStartedDomainEventHandler(
            IBuyerRepository buyerRepository,
            IBuyerIntegrationEventService buyerIntegrationEventService)
        {
            _buyerRepository = buyerRepository ?? throw new ArgumentNullException(nameof(buyerRepository));
           _buyerIntegrationEventService = buyerIntegrationEventService ?? throw new ArgumentNullException(nameof(orderingIntegrationEventService));
        }

        private object orderingIntegrationEventService()
        {
            throw new NotImplementedException();
        }

        public Task Handle(BuyStartedDomainEvent notification, CancellationToken cancellationToken)
        {
            return null;
        }
    }
}
