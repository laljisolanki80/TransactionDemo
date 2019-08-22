using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Transaction.Infrastructure.Migrations
{
    public partial class Initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BuyerDatas",
                columns: table => new
                {
                    BuyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BuyPrice = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    BuyQuantity = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    InsertTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RemainingQuantity = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    SettledQuantity = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    TransactionStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuyerDatas", x => x.BuyId);
                });

            migrationBuilder.CreateTable(
                name: "Ledgers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BuyerId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BuyerPrice = table.Column<double>(type: "float", nullable: false),
                    DisplayId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ProcessTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SellerId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SellerPrice = table.Column<double>(type: "float", nullable: false),
                    SellerQuantity = table.Column<long>(type: "bigint", nullable: false),
                    TransactionStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ledgers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SellerDatas",
                columns: table => new
                {
                    SellerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InsertTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RemainingQuantity = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    SellPrice = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    SellQuantity = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    SettledQuantity = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    TransactionStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellerDatas", x => x.SellerId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BuyerDatas");

            migrationBuilder.DropTable(
                name: "Ledgers");

            migrationBuilder.DropTable(
                name: "SellerDatas");
        }
    }
}
