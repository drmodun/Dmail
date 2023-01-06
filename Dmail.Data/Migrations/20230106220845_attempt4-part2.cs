using Dmail.Data.Enums;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dmail.Data.Migrations
{
    public partial class attempt4part2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<EventAnswer>(
                name: "Answer",
                table: "MessagesReceivers",
                type: "event_answer",
                nullable: false,
                defaultValue: EventAnswer.None);

            migrationBuilder.UpdateData(
                table: "MessagesReceivers",
                keyColumns: new[] { "MessageId", "ReceiverId" },
                keyValues: new object[] { 4, 1 },
                column: "Answer",
                value: EventAnswer.Accepted);

            migrationBuilder.UpdateData(
                table: "MessagesReceivers",
                keyColumns: new[] { "MessageId", "ReceiverId" },
                keyValues: new object[] { 4, 2 },
                column: "Answer",
                value: EventAnswer.Rejected);

            migrationBuilder.UpdateData(
                table: "MessagesReceivers",
                keyColumns: new[] { "MessageId", "ReceiverId" },
                keyValues: new object[] { 5, 4 },
                column: "Answer",
                value: EventAnswer.Accepted);

            migrationBuilder.UpdateData(
                table: "MessagesReceivers",
                keyColumns: new[] { "MessageId", "ReceiverId" },
                keyValues: new object[] { 5, 5 },
                column: "Answer",
                value: EventAnswer.Accepted);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Answer",
                table: "MessagesReceivers");
        }
    }
}
