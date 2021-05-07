using Microsoft.EntityFrameworkCore.Migrations;

namespace Smile.API.Migrations
{
    public partial class RemoveUrlColumnFromFiles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "ReportFiles");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "ReplyFiles");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Files");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "ReportFiles",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "ReplyFiles",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Files",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);
        }
    }
}
