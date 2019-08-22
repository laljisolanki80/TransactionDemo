using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Transaction.Domain.SeedWork
{
    public class Entity
    {
        long _Id;
        public virtual long Id
        {
            get
            { return _Id; }
            set
            {
                _Id = value;
            }
        }
    
    //int _Id;
    //public virtual int Id
    //{
    //    get
    //    {
    //        return _Id;
    //    }
    //    protected set
    //    {
    //        _Id = value;
    //    }
    //}
    //private List<INotification> _domainEvents;
    //public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();
    //public void ClearDomainEvents()
    //{
    //    _domainEvents?.Clear();
    //}

    //public void AddDomainEvent(INotification eventItem)
    //{
    //    _domainEvents = _domainEvents ?? new List<INotification>();
    //    _domainEvents.Add(eventItem);
    //}
    }
}
