using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Board.Host.DbMigrator.Migrations
{
    /// <inheritdoc />
    public partial class AddRoleId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Role_UserRoleRoleId",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "UserRoleRoleId",
                table: "User",
                newName: "RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_User_UserRoleRoleId",
                table: "User",
                newName: "IX_User_RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Role_RoleId",
                table: "User",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Role_RoleId",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "User",
                newName: "UserRoleRoleId");

            migrationBuilder.RenameIndex(
                name: "IX_User_RoleId",
                table: "User",
                newName: "IX_User_UserRoleRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Role_UserRoleRoleId",
                table: "User",
                column: "UserRoleRoleId",
                principalTable: "Role",
                principalColumn: "RoleId");
        }
    }
}
