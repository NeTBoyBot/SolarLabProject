using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Board.Host.DbMigrator.Migrations
{
    /// <inheritdoc />
    public partial class AddTelegramClient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TelegramClientId",
                table: "Category",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TelegramClient",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    ChatId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramClient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TelegramClient_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Category_TelegramClientId",
                table: "Category",
                column: "TelegramClientId");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramClient_UserId",
                table: "TelegramClient",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Category_TelegramClient_TelegramClientId",
                table: "Category",
                column: "TelegramClientId",
                principalTable: "TelegramClient",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_TelegramClient_TelegramClientId",
                table: "Category");

            migrationBuilder.DropTable(
                name: "TelegramClient");

            migrationBuilder.DropIndex(
                name: "IX_Category_TelegramClientId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "TelegramClientId",
                table: "Category");
        }
    }
}
