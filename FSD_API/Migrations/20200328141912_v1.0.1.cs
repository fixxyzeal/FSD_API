using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FSD_API.Migrations
{
    public partial class v101 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PhoneRanking",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Ranking = table.Column<int>(nullable: false),
                    DeviceName = table.Column<string>(maxLength: 100, nullable: true),
                    OS = table.Column<string>(maxLength: 50, nullable: true),
                    Ram = table.Column<int>(nullable: false),
                    StorageSize = table.Column<int>(nullable: false),
                    GPU = table.Column<int>(nullable: false),
                    MEM = table.Column<int>(nullable: false),
                    UX = table.Column<int>(nullable: false),
                    TotalScore = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    UpdateBy = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneRanking", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhoneRankingHistorie",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Ranking = table.Column<int>(nullable: false),
                    DeviceName = table.Column<string>(maxLength: 100, nullable: true),
                    OS = table.Column<string>(maxLength: 50, nullable: true),
                    Ram = table.Column<int>(nullable: false),
                    StorageSize = table.Column<int>(nullable: false),
                    GPU = table.Column<int>(nullable: false),
                    MEM = table.Column<int>(nullable: false),
                    UX = table.Column<int>(nullable: false),
                    TotalScore = table.Column<int>(nullable: false),
                    RankingDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    UpdateBy = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneRankingHistorie", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhoneRanking_Ranking_DeviceName_OS_Ram_StorageSize",
                table: "PhoneRanking",
                columns: new[] { "Ranking", "DeviceName", "OS", "Ram", "StorageSize" });

            migrationBuilder.CreateIndex(
                name: "IX_PhoneRankingHistorie_Ranking_DeviceName_OS_Ram_StorageSize_~",
                table: "PhoneRankingHistorie",
                columns: new[] { "Ranking", "DeviceName", "OS", "Ram", "StorageSize", "RankingDate" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhoneRanking");

            migrationBuilder.DropTable(
                name: "PhoneRankingHistorie");
        }
    }
}
