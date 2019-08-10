using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Transaction.Domain.AggreagatesModels.BuyerAggregate;

namespace Transaction.Domain.Events
{
    // Event used when an Buyer is created
    public class BuyStartedDomainEvent : INotification
    {
        public string Id { get; }
        public decimal Price { get; }
        public decimal Quantity { get; }
        public Buyer Buyer { get; }

        public BuyStartedDomainEvent(Buyer buyer,string id,decimal price,decimal quantity)
        {
            Buyer = buyer;
            Id = id;
            Price = price;
            Quantity = quantity;
        }
    }
}
