using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Transaction.Domain.SeedWork
{
    public abstract class Entity
    {
        private List<INotification> _domainEvents;
        public void AddDomainEvent(INotification eventItem)
        {
            _domainEvents = _domainEvents ?? new List<INotification>();
            _domainEvents.Add(eventItem);
        }
    }
}
