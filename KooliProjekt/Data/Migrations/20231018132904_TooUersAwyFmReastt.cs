using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KooliProjekt.Data.Migrations
{
    public partial class TooUersAwyFmReastt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RealEstates_Users_UserId",
                table: "RealEstates");

            migrationBuilder.DropIndex(
                name: "IX_RealEstates_UserId",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "RealEstates");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "RealEstates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_RealEstates_UserId",
                table: "RealEstates",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_RealEstates_Users_UserId",
                table: "RealEstates",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
