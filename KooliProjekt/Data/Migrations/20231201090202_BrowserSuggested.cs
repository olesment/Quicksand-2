using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KooliProjekt.Data.Migrations
{
    public partial class BrowserSuggested : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LossOrPtofit",
                table: "Transactions",
                newName: "LossOrProfit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LossOrProfit",
                table: "Transactions",
                newName: "LossOrPtofit");
        }
    }
}
