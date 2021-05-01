using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Smile.API.Migrations
{
    public partial class AddFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "ReportFiles");

            migrationBuilder.DropColumn(
                name: "FileUrl",
                table: "ReportFiles");

            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "ReplyFiles");

            migrationBuilder.DropColumn(
                name: "FileUrl",
                table: "ReplyFiles");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "ReportFiles",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "ReportFiles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "ReportFiles",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "ReplyFiles",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "ReplyFiles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "ReplyFiles",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Url = table.Column<string>(nullable: true),
                    Path = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "ReportFiles");

            migrationBuilder.DropColumn(
                name: "Path",
                table: "ReportFiles");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "ReportFiles");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "ReplyFiles");

            migrationBuilder.DropColumn(
                name: "Path",
                table: "ReplyFiles");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "ReplyFiles");

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "ReportFiles",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileUrl",
                table: "ReportFiles",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "ReplyFiles",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileUrl",
                table: "ReplyFiles",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);
        }
    }
}
