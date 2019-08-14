using System;
using System.Collections.Generic;
using System.Text;
using Transaction.Domain.SeedWork;

namespace Transaction.Domain.AggreagatesModels.SellerAggregate
{
    public class Seller:Entity,IAggregateRoot
    {
        private DateTime _sellDate;
        private int? _sellerId;

        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public int? GetSellerId => _sellerId;
        protected Seller(string id, decimal price, decimal quantity, int? sellerId = null) : this()
        {
            _sellerId = sellerId;
            _sellDate = DateTime.UtcNow;
           // AddSellDomainEvent(id,price,quantity);
        }

        public Seller()
        {
        }
    }
}
