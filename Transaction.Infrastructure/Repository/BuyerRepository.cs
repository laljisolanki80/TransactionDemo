using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction.Domain.AggreagatesModels.Aggregate;
using Transaction.Domain.IRepository;
using Transaction.Infrastructure.DataBase;

namespace Transaction.Infrastructure.Repository
{
    public class BuyerRepository:IBuyerRepository
    {
        private TransactionDbContext _transactionDbContext;
        public BuyerRepository(TransactionDbContext transactionDbContext)
        {
            _transactionDbContext = transactionDbContext;
        }
        public async Task AddBuyerData(BuyerData buyerData)
        {
            _transactionDbContext.BuyerDatas.Add(buyerData);
            await _transactionDbContext.SaveChangesAsync();
        }
        public async Task GetBuyerTransactionAsync()
        {
            foreach (var buy in _transactionDbContext.BuyerDatas)
            {
                Console.WriteLine(buy.BuyPrice + " : " + buy.BuyQuantity + " : " + buy.InsertTime);
                Console.WriteLine("------------------------------------------------------");
                var compare = from sellRaw in _transactionDbContext.SellerDatas
                              where sellRaw.SellPrice == buy.BuyPrice
                              orderby sellRaw.InsertTime
                              select sellRaw;

                var Sellerlist = compare.ToList();
                foreach (var sell in Sellerlist)
                {
                    decimal Quantities = 0.0m;
                    if (buy.RemainingQuantity >= sell.RemainingQuantity)
                    {
                        Quantities = sell.RemainingQuantity;
                    }
                    if (sell.RemainingQuantity >= buy.RemainingQuantity)
                    {
                        Quantities = buy.RemainingQuantity;
                    }
                    if (buy.RemainingQuantity >= sell.RemainingQuantity)
                    {
                        buy.RemainingQuantity -= sell.RemainingQuantity;
                        buy.SettledQuantity += sell.RemainingQuantity;
                        sell.SettledQuantity = sell.SettledQuantity + sell.RemainingQuantity;
                        sell.RemainingQuantity = sell.SellQuantity - sell.SettledQuantity;
                        if (sell.RemainingQuantity == 0 || sell.RemainingQuantity > 0
                            || buy.RemainingQuantity > 0 || buy.RemainingQuantity == 0)
                        {
                            if (sell.RemainingQuantity == 0)
                            {
                                sell.TransactionStatus = TransactionStatus.Success;
                            }
                            if (sell.RemainingQuantity > 0)
                            {
                                sell.TransactionStatus = TransactionStatus.Hold;
                            }
                            if (buy.RemainingQuantity == 0)
                            {
                                buy.TransactionStatus = TransactionStatus.Success;
                            }
                            if (buy.RemainingQuantity > 0)
                            {
                                buy.TransactionStatus = TransactionStatus.Hold;
                            }
                            else
                            {
                                sell.TransactionStatus = TransactionStatus.OperatorFail;
                                buy.TransactionStatus = TransactionStatus.OperatorFail;
                            }
                        }
                        else
                        {
                            sell.TransactionStatus = TransactionStatus.SystemFail;
                            buy.TransactionStatus = TransactionStatus.SystemFail;
                        }
                    }
                    if (buy.RemainingQuantity < sell.RemainingQuantity)
                    {
                        sell.RemainingQuantity -= buy.RemainingQuantity;
                        sell.SettledQuantity = sell.SellQuantity - sell.RemainingQuantity;
                        if (sell.RemainingQuantity > 0)
                        {
                            buy.SettledQuantity += buy.RemainingQuantity;
                            buy.RemainingQuantity = 0;
                            sell.TransactionStatus = TransactionStatus.Hold;
                            if (buy.RemainingQuantity == 0)
                            {
                                buy.TransactionStatus = TransactionStatus.Success;
                            }
                            else
                            {
                                buy.TransactionStatus = TransactionStatus.Hold;
                            }
                        }
                        else
                        {
                            sell.TransactionStatus = TransactionStatus.Success;
                        }
                    }


                    _transactionDbContext.BuyerDatas.Update(buy);
                    _transactionDbContext.SellerDatas.Update(sell);

                    //Ledger ledger = new Ledger();
                    //ledger.BuyerId = buy.BuyId;
                    //ledger.DisplayId = Guid.NewGuid().ToString();
                    //ledger.BuyerPrice = (double)buy.BuyPrice;
                    //ledger.SellerId = sell.Id;
                    //ledger.SellerPrice = (double)sell.Price;
                    //ledger.SellerQuantity = (long)Quantities;
                    //ledger.ProcessTime = DateTime.Now;
                    //ledger.Status = Status.Success;

                    //_transactionDbContext.Ledgers.Add(ledger);

                }
                _transactionDbContext.SaveChanges();
            }
        }
    }
}
