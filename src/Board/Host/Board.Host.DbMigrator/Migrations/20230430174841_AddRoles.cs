using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Board.Host.DbMigrator.Migrations
{
    /// <inheritdoc />
    public partial class AddRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserRoleRoleId",
                table: "User",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleName = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.RoleId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_UserRoleRoleId",
                table: "User",
                column: "UserRoleRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Role_UserRoleRoleId",
                table: "User",
                column: "UserRoleRoleId",
                principalTable: "Role",
                principalColumn: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Role_UserRoleRoleId",
                table: "User");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropIndex(
                name: "IX_User_UserRoleRoleId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "UserRoleRoleId",
                table: "User");
        }
    }
}
