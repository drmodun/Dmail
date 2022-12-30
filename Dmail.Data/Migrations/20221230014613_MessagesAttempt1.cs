using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dmail.Data.Migrations
{
    public partial class MessagesAttempt1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Messages",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_UserId",
                table: "Messages",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_UserId",
                table: "Messages",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_UserId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_UserId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Messages");
        }
    }
}
