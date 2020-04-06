using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FSD_API.Migrations
{
    public partial class v103 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Action",
                columns: table => new
                {
                    ActionId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<string>(maxLength: 100, nullable: true),
                    UserDisplayName = table.Column<string>(maxLength: 200, nullable: true),
                    Message = table.Column<string>(maxLength: 50, nullable: true),
                    Platform = table.Column<string>(nullable: true),
                    ActionDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    UpdateBy = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Action", x => x.ActionId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Action");
        }
    }
}
