using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Transaction.Infrastructure.Database
{
    public sealed class BuyerDbContext : DbContext
    {
        //public DbSet<Id> Guids { get; set }
        //public DbSet<Price> Prices { get; set; }
        //public DbSet<Amount> Amounts { get; set; }
        //public DbSet<TransactionStatus> TransactionStatuses { get; set; }
        //public DbSet<TimeStamp> TimeStamps { get; set; }

        private BuyerDbContext(DbContextOptions<BuyerDbContext> options) : base(options) { }

    }
}
