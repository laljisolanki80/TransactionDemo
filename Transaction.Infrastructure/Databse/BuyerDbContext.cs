using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Transaction.Domain.AggreagatesModels.BuyerAggregate;
using Transaction.Domain.SeedWork;

namespace Transaction.Infrastructure.Database
{
    public class BuyerDbContext : DbContext    //IUnitOfWork Add letter by akshay
    {
        //add BuyerDbContext by Lalji 5:10PM 10/08/2019
        public BuyerDbContext(DbContextOptions<BuyerDbContext> options) : base(options) { }
        public DbSet<BuyerInformartion> BuyerInformartions { get; set; }
        public DbSet<Buyer> Buyers { get; set; }

        //public DbSet<BuyerTransactionStatus> BuyerTransactionStatuse { get; set; }
    }
}
