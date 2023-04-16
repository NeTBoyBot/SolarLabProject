using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Board.Host.DbMigrator.Migrations
{
    /// <inheritdoc />
    public partial class AddCommentRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_User_Id",
                table: "Comment");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_SenderId",
                table: "Comment",
                column: "SenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_User_SenderId",
                table: "Comment",
                column: "SenderId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_User_SenderId",
                table: "Comment");

            migrationBuilder.DropIndex(
                name: "IX_Comment_SenderId",
                table: "Comment");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_User_Id",
                table: "Comment",
                column: "Id",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
