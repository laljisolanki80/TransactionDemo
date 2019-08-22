using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Transaction.Domain.Events;
using Transaction.Domain.SeedWork;

namespace Transaction.Domain.AggreagatesModels.SellerAggregate
{
    public class Seller:Entity,IAggregateRoot
    {
        private DateTime _sellDate;
        private int? _sellerId;
        private int? sellerId;
        private List<SellerItem> _sellerItems;

        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public int? GetSellerId => _sellerId;
        public Seller(Guid id)
        {
            _sellerItems = new List<SellerItem>();
        }
        //protected Seller(string id, decimal price, decimal quantity, int? sellerId = null) : this()
        //{
        //    _sellerId = sellerId;
        //    _sellDate = DateTime.UtcNow;
        //    AddSellStartedDomainEvent(id,price,quantity);
        //}by lalji remove comment
        private void AddSellStartedDomainEvent(string id, decimal price, decimal quantity)
        {
            var sellStartedDomainEvent = new SellStartedDomainEvent(this, id, price, quantity);
            this.AddDomainEvent(sellStartedDomainEvent);

        }
        public decimal GetTotal()
        {
            return _sellerItems.Sum(o => o.GetUnits() * o.GetUnitPrice());
        }
    }
}
