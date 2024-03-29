﻿using MediatR;
using System.Collections.Generic;

namespace Transaction.Domain.AggreagatesModels.Aggregate
{
    public class Entity
    {
        //long _Id;
        //public virtual long Id
        //{
        //    get
        //    { return _Id; }
        //    set
        //    {
        //        _Id = value;
        //    }
        //}
        private List<INotification> _domainEvents;
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();

        public void AddDomainEvent(INotification eventItem)
        {
            _domainEvents = _domainEvents ?? new List<INotification>();
            _domainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(INotification eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }
    }
}