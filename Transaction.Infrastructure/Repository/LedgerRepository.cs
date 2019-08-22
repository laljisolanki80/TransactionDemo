using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Transaction.Domain.AggreagatesModels.Aggregate;
using Transaction.Domain.IRepository;

namespace Transaction.Infrastructure.Repository
{
    public class LedgerRepository:ILedgerRepository
    {
       public async Task AddLedgerData(SellerData sell,BuyerData buy,decimal Quantities)
        {
            Ledger ledger = new Ledger();
            ledger.BuyerId = buy.BuyId.ToString();
            ledger.DisplayId = Guid.NewGuid().ToString();
            ledger.BuyerPrice = (double)buy.BuyPrice;
            ledger.SellerId = sell.SellerId.ToString();
            ledger.SellerQuantity = (long)Quantities;
            ledger.ProcessTime = DateTime.Now;
            ledger.TransactionStatus = TransactionStatus.Success;
           
        }
    }
}
