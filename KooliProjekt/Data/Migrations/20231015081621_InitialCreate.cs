using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KooliProjekt.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RealEstates",
                columns: table => new
                {
                    RealEstateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RealEstateName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RealEstateCountry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RealEstateCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RealEstateAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PurchasePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CurrentValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LastCurrentValueChangeTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CurrentlyOwned = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RealEstates", x => x.RealEstateId);
                    table.ForeignKey(
                        name: "FK_RealEstates_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    StockId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StockName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StockTicker = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StockInvestmentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StockAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    StockBuyingPrice = table.Column<int>(type: "int", nullable: true),
                    InvestedInParticluarStock = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LastClosingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastClosingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    CurrentlyOwned = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.StockId);
                    table.ForeignKey(
                        name: "FK_Stocks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UsersPortfolios",
                columns: table => new
                {
                    PortfolioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    SnapshotDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InvestmentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalInvested = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CurrentAggregateValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    InvestedInStocksValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CurrentAggregateStockValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    InvestedInRealestate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CurrentAggregateRealEstateValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FreeFunds = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersPortfolios", x => x.PortfolioId);
                    table.ForeignKey(
                        name: "FK_UsersPortfolios_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InvestmentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssetId = table.Column<int>(type: "int", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransactedAmount = table.Column<int>(type: "int", nullable: true),
                    TransactionUnitCost = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TransactionResult = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PortfolioId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_Transactions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_UsersPortfolios_PortfolioId",
                        column: x => x.PortfolioId,
                        principalTable: "UsersPortfolios",
                        principalColumn: "PortfolioId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RealEstates_UserId",
                table: "RealEstates",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_UserId",
                table: "Stocks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_PortfolioId",
                table: "Transactions",
                column: "PortfolioId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_UserId",
                table: "Transactions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersPortfolios_UserId",
                table: "UsersPortfolios",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RealEstates");

            migrationBuilder.DropTable(
                name: "Stocks");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "UsersPortfolios");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
