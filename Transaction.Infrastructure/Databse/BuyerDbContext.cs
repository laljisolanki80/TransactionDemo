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
    //IUnitOfWork Add letter by akshay
    public class TransactionDbContext : DbContext    //DbContext class rename by Lalji previous BuyerDbContext
    {
        //add BuyerDbContext by Lalji 5:10PM 10/08/2019
        public TransactionDbContext(DbContextOptions<TransactionDbContext> options) : base(options) { }
        public DbSet<BuyerInformartion> BuyerInformartions { get; set; }
        public DbSet<Buyer> Buyers { get; set; }

        //public DbSet<BuyerTransactionStatus> BuyerTransactionStatuse { get; set; }
    }
}
