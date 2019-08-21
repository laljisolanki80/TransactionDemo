using System;
using System.Collections.Generic;
using System.Text;
using Transaction.Domain.SeedWork;

namespace Transaction.Domain.AggreagatesModels.SellerAggregate
{
    public class SellerItem : Entity
    {
        private decimal _price;
        private decimal _quantity;
        public SellerItem()
        {

        }
        public SellerItem(decimal unitPrice, decimal unitQuantity = 1)
        {
            if (unitQuantity <= 0)
            {
            }
            _price = unitPrice;
            _quantity = unitQuantity;
        }
        public decimal GetUnits()
        {
            return _quantity;
        }
        public decimal GetUnitPrice()
        {
            return _price;
        }
        public void AddUnits(decimal unitQuantity)
        {
            if (unitQuantity < 0)
            {
            }

            _quantity += unitQuantity;
        }
    }

}
