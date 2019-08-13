﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Transaction.Domain.AggreagatesModels.BuyerAggregate;
using Transaction.Domain.Events;

namespace Transaction.API.Application.DomainEventsHandler
{
    public class ValidateOrAddBuyerWhenBuyerStartedDomainEventHandler
        : INotificationHandler<BuyStartedDomainEvent>
    {
        private readonly IBuyerRepository _buyerRepository;
        private readonly IBuyerIntegrationEventService _buyerIntegrationEventService;

        public Task Handle(BuyStartedDomainEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}