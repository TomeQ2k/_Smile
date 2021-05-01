using Microsoft.EntityFrameworkCore.Migrations;

namespace Smile.API.Migrations
{
    public partial class AddGroupInvite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsInvited",
                table: "GroupMembers");

            migrationBuilder.CreateTable(
                name: "GroupInvites",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    GroupId = table.Column<string>(nullable: false),
                    IsJoining = table.Column<bool>(nullable: false),
                    IsInvited = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupInvites", x => new { x.UserId, x.GroupId });
                    table.ForeignKey(
                        name: "FK_GroupInvites_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupInvites_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupInvites_GroupId",
                table: "GroupInvites",
                column: "GroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupInvites");

            migrationBuilder.AddColumn<bool>(
                name: "IsInvited",
                table: "GroupMembers",
                type: "bit(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
