using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Transaction.Domain.AggreagatesModels.SellerAggregate;

namespace Transaction.Domain.Events
{
    public class SellStartedDomainEvent:INotification
    {
        public string Id { get; }
        public decimal Price { get; }
        public decimal Quantity { get; }
        public Seller Seller { get; }

        public SellStartedDomainEvent(Seller seller, string id, decimal price, decimal quantity)
        {
            Seller = seller;
            Id = id;
            Price = price;
            Quantity = quantity;
        }
    }
}
