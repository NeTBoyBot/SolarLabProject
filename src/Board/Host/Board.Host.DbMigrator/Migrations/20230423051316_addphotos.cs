using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Board.Host.DbMigrator.Migrations
{
    /// <inheritdoc />
    public partial class addphotos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KodBase64",
                table: "User");

            migrationBuilder.AddColumn<Guid>(
                name: "PhotoId",
                table: "User",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Photo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Base64 = table.Column<string>(type: "text", nullable: true),
                    AdId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photo_Ad_AdId",
                        column: x => x.AdId,
                        principalTable: "Ad",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_PhotoId",
                table: "User",
                column: "PhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_Photo_AdId",
                table: "Photo",
                column: "AdId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Photo_PhotoId",
                table: "User",
                column: "PhotoId",
                principalTable: "Photo",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Photo_PhotoId",
                table: "User");

            migrationBuilder.DropTable(
                name: "Photo");

            migrationBuilder.DropIndex(
                name: "IX_User_PhotoId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "PhotoId",
                table: "User");

            migrationBuilder.AddColumn<string>(
                name: "KodBase64",
                table: "User",
                type: "text",
                nullable: true);
        }
    }
}
