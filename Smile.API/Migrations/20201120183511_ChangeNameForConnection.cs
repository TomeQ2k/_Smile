using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Smile.API.Migrations
{
    public partial class ChangeNameForConnection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SignalrConnections");

            migrationBuilder.CreateTable(
                name: "Connections",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    ConnectionId = table.Column<string>(nullable: false),
                    DateEstablished = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Connections", x => new { x.UserId, x.ConnectionId });
                    table.ForeignKey(
                        name: "FK_Connections_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Connections");

            migrationBuilder.CreateTable(
                name: "SignalrConnections",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: false),
                    ConnectionId = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: false),
                    DateEstablished = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SignalrConnections", x => new { x.UserId, x.ConnectionId });
                    table.ForeignKey(
                        name: "FK_SignalrConnections_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
