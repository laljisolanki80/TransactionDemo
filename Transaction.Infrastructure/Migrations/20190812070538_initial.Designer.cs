﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using Transaction.Infrastructure.Database;

namespace Transaction.Infrastructure.Migrations
{
    [DbContext(typeof(TransactionDbContext))]
    [Migration("20190812070538_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Transaction.Infrastructure.Database.BuyerInformartion", b =>
                {
                    b.Property<int>("BuyerId")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount");

                    b.Property<decimal>("Price");

                    b.Property<byte[]>("TimeStamps")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<decimal>("Total");

                    b.Property<int>("TransationStatus");

                    b.HasKey("BuyerId");

                    b.ToTable("BuyerInformartion","dbo");
                });
#pragma warning restore 612, 618
        }
    }
}
