﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Transaction.Domain.DomainEvents;

namespace Transaction.API.Application.DomainEventHandlers
{
    public class TransactionCancelDomainEventHandler : INotificationHandler<TransactionCancelDomainEvent>
    {
        public Task Handle(TransactionCancelDomainEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}