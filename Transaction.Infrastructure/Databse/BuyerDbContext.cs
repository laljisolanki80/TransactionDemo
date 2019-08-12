using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Transaction.Domain.AggreagatesModels.BuyerAggregate;

namespace Transaction.Infrastructure.Database
{
    public class BuyerDbContext : DbContext
    {
        //add BuyerDbContext by Lalji 5:10PM 10/08/2019
        public BuyerDbContext(DbContextOptions<BuyerDbContext> options) : base(options) { }
        public DbSet<BuyerInformartion> BuyerInformartions { get; set; }
    }

    [Table("BuyerInformartion", Schema = "dbo")]
    public class BuyerInformartion
    {
        [Key] 
        public int BuyerId { get; set; }

        [Display(Name = "Price(INR)")]
        public decimal Price{ get; set; }
        public decimal  Amount{ get; set; }
        public decimal Total { get; set; }
        public int TransationStatus { get; set; }
        [Timestamp]
        public byte[] TimeStamps { get; set; }
    }
}
