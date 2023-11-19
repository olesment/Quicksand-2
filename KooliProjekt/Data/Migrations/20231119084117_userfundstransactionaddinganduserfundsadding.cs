using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KooliProjekt.Data.Migrations
{
    public partial class userfundstransactionaddinganduserfundsadding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FundsTransaction",
                table: "FundsTransaction");

            migrationBuilder.RenameTable(
                name: "FundsTransaction",
                newName: "UserFundsTransactions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserFundsTransactions",
                table: "UserFundsTransactions",
                column: "FundsTransactionId");

            migrationBuilder.CreateTable(
                name: "UserFunds",
                columns: table => new
                {
                    FundID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FundName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DepositedFunds = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LockedFunds = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    WithdrawnFunds = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFunds", x => x.FundID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserFunds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserFundsTransactions",
                table: "UserFundsTransactions");

            migrationBuilder.RenameTable(
                name: "UserFundsTransactions",
                newName: "FundsTransaction");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FundsTransaction",
                table: "FundsTransaction",
                column: "FundsTransactionId");
        }
    }
}
