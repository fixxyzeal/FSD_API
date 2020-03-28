using Microsoft.EntityFrameworkCore.Migrations;

namespace FSD_API.Migrations
{
    public partial class v102 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CPU",
                table: "PhoneRankingHistorie",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CPU",
                table: "PhoneRanking",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CPU",
                table: "PhoneRankingHistorie");

            migrationBuilder.DropColumn(
                name: "CPU",
                table: "PhoneRanking");
        }
    }
}
