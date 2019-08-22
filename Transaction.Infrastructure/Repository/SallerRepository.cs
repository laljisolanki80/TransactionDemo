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
    public class SallerRepository: ISellerRepository
    {
        private TransactionDbContext _transactionDbContext;
        public SallerRepository(TransactionDbContext transactionDbContext)
        {
            _transactionDbContext = transactionDbContext;
        }
        public async Task AddSellerData(SellerData sellerData)
        {
            _transactionDbContext.SellerDatas.Add(sellerData);
            await _transactionDbContext.SaveChangesAsync();
        }
        public async Task GetSellerTransactionAsync()
        {
            foreach (var sell in _transactionDbContext.SellerDatas)
            {
                Console.WriteLine(sell.SellPrice + " : " + sell.SellQuantity + " : " + sell.InsertTime);
                Console.WriteLine("------------------------------------------------------");
                var compare = from buyRaw in _transactionDbContext.BuyerDatas
                              where buyRaw.BuyPrice == sell.SellPrice
                              orderby buyRaw.InsertTime
                              select buyRaw;

                var Buyerlist = compare.ToList();
                foreach (var buy in Buyerlist)
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
                    if (sell.RemainingQuantity >= buy.RemainingQuantity)
                    {
                        sell.RemainingQuantity -= buy.RemainingQuantity;
                        sell.SettledQuantity += buy.RemainingQuantity;
                        buy.SettledQuantity = buy.SettledQuantity + buy.RemainingQuantity;
                        buy.RemainingQuantity = buy.BuyQuantity - buy.SettledQuantity;
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
                    if (sell.RemainingQuantity < buy.RemainingQuantity)
                    {
                        buy.RemainingQuantity -= sell.RemainingQuantity;
                        buy.SettledQuantity = buy.BuyQuantity - buy.RemainingQuantity;
                        if (buy.RemainingQuantity > 0)
                        {
                            sell.SettledQuantity += sell.RemainingQuantity;
                            sell.RemainingQuantity = 0;
                            buy.TransactionStatus = TransactionStatus.Hold;
                            if (sell.RemainingQuantity == 0)
                            {
                                sell.TransactionStatus = TransactionStatus.Success;
                            }
                            else
                            {
                                sell.TransactionStatus = TransactionStatus.Hold;
                            }
                        }
                        else
                        {
                            buy.TransactionStatus = TransactionStatus.Success;
                        }
                    }


                    _transactionDbContext.SellerDatas.Update(sell);
                    _transactionDbContext.BuyerDatas.Update(buy);

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
