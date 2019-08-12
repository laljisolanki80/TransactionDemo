using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Transaction.Domain.AggreagatesModels.BuyerAggregate;

namespace Transaction.Infrastructure.Database
{
    public sealed class BuyerDbContext : DbContext
    {
        public DbSet<Buyer> Buyers  { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<Quantity> Quantities { get; set; }
        public DbSet<BuyerTransactionStatus> BuyerTransactionStatuses { get; set; }
        public DbSet<TimeStamp> TimeStamps { get; set; }

        //private BuyerDbContext(DbContextOptions<BuyerDbContext> options) : base(options) { }

    }

    public class TimeStamp
    {
        [Timestamp]
        public byte[] TimeStamps { get; set; } 
    }

    public class Price
    {
        public  decimal Prices { get; set; }
    }
    public class Quantity
    {
        public decimal Quantities { get; set; }
    }
}
