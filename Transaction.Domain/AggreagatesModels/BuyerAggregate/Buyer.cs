using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using Transaction.Domain.Events;
using Transaction.Domain.SeedWork;

namespace Transaction.Domain.AggreagatesModels.BuyerAggregate
{
    public class Buyer:Entity
    {
        //private DateTime _buyDate;
        public int? GetBuyerId => _buyerId;
        private int? _buyerId;
        //private int _orderStatusId;
        private readonly List<BuyerItem> _buyerItems;
        protected Buyer()
        { }
   
    public Buyer(string id, decimal price,decimal quantity, int? buyerId = null)
        {
            _buyerId = buyerId;
            AddBuyStartedDomainEvent(id, price, quantity);
        }

        private void AddBuyStartedDomainEvent(string id, decimal price, decimal quantity)
        {
            var buyStartedDomainEvent = new BuyStartedDomainEvent(this, id, price, quantity);
            this.AddDomainEvent(buyStartedDomainEvent);

        }
        public decimal GetTotal()
        {
            //example total should be 140 INR = 2 USD*70 INR by lalji
            return _buyerItems.Sum(o => o.GetUnits() * o.GetUnitPrice());
        }
    }
}
