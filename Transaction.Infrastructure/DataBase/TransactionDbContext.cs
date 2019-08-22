using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Transaction.Domain.AggreagatesModels.Aggregate;

namespace Transaction.Infrastructure.DataBase
{
    public class TransactionDbContext:DbContext
    {
        public TransactionDbContext(DbContextOptions<TransactionDbContext> options) : base(options)
        {

        }
        public DbSet<SellerData> SellerDatas { get; set; }
        public DbSet<BuyerData> BuyerDatas { get; set; }
        public DbSet<Ledger> Ledgers { get; set; }
    }
}
