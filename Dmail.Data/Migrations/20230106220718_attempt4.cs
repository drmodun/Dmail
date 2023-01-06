using Dmail.Data.Enums;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dmail.Data.Migrations
{
    public partial class attempt4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Accepted",
                table: "MessagesReceivers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<EventAnswer>(
                name: "Accepted",
                table: "MessagesReceivers",
                type: "event_answer",
                nullable: false,
                defaultValue: EventAnswer.None);

            migrationBuilder.UpdateData(
                table: "MessagesReceivers",
                keyColumns: new[] { "MessageId", "ReceiverId" },
                keyValues: new object[] { 4, 1 },
                column: "Accepted",
                value: EventAnswer.Accepted);

            migrationBuilder.UpdateData(
                table: "MessagesReceivers",
                keyColumns: new[] { "MessageId", "ReceiverId" },
                keyValues: new object[] { 4, 2 },
                column: "Accepted",
                value: EventAnswer.Rejected);

            migrationBuilder.UpdateData(
                table: "MessagesReceivers",
                keyColumns: new[] { "MessageId", "ReceiverId" },
                keyValues: new object[] { 5, 4 },
                column: "Accepted",
                value: EventAnswer.Accepted);

            migrationBuilder.UpdateData(
                table: "MessagesReceivers",
                keyColumns: new[] { "MessageId", "ReceiverId" },
                keyValues: new object[] { 5, 5 },
                column: "Accepted",
                value: EventAnswer.Accepted);
        }
    }
}
