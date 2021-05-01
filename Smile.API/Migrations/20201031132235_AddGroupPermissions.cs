using Microsoft.EntityFrameworkCore.Migrations;

namespace Smile.API.Migrations
{
    public partial class AddGroupPermissions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InviteMemberPermission",
                table: "Groups",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RemoveMemberPermission",
                table: "Groups",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InviteMemberPermission",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "RemoveMemberPermission",
                table: "Groups");
        }
    }
}
