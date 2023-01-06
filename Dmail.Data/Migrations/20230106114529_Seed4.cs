using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dmail.Data.Migrations
{
    public partial class Seed4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateOfEvent",
                value: new DateTime(2023, 1, 6, 11, 45, 29, 396, DateTimeKind.Utc).AddTicks(4963));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateOfEvent",
                value: new DateTime(2023, 1, 6, 11, 45, 29, 396, DateTimeKind.Utc).AddTicks(5011));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateOfEvent",
                value: new DateTime(2023, 1, 6, 11, 45, 29, 396, DateTimeKind.Utc).AddTicks(5017));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 6,
                column: "DateOfEvent",
                value: new DateTime(2023, 1, 6, 11, 45, 29, 396, DateTimeKind.Utc).AddTicks(5036));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 7,
                column: "DateOfEvent",
                value: new DateTime(2023, 1, 6, 11, 45, 29, 396, DateTimeKind.Utc).AddTicks(5040));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 9,
                column: "DateOfEvent",
                value: new DateTime(2023, 1, 6, 11, 45, 29, 396, DateTimeKind.Utc).AddTicks(5051));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 10,
                column: "DateOfEvent",
                value: new DateTime(2023, 1, 6, 11, 45, 29, 396, DateTimeKind.Utc).AddTicks(5056));

            migrationBuilder.InsertData(
                table: "MessagesReceivers",
                columns: new[] { "MessageId", "ReceiverId", "Accepted", "Read" },
                values: new object[,]
                {
                    { 3, 1, false, true },
                    { 4, 1, true, true },
                    { 9, 1, false, true },
                    { 1, 2, false, true },
                    { 3, 2, false, true },
                    { 4, 2, false, true },
                    { 6, 2, false, false },
                    { 4, 4, false, false },
                    { 5, 4, true, true },
                    { 6, 4, false, true },
                    { 7, 4, false, true },
                    { 2, 5, false, false },
                    { 5, 5, true, true },
                    { 5, 6, false, true },
                    { 3, 7, false, true },
                    { 7, 7, false, false },
                    { 8, 7, false, false },
                    { 10, 10, false, false }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MessagesReceivers",
                keyColumns: new[] { "MessageId", "ReceiverId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "MessagesReceivers",
                keyColumns: new[] { "MessageId", "ReceiverId" },
                keyValues: new object[] { 4, 1 });

            migrationBuilder.DeleteData(
                table: "MessagesReceivers",
                keyColumns: new[] { "MessageId", "ReceiverId" },
                keyValues: new object[] { 9, 1 });

            migrationBuilder.DeleteData(
                table: "MessagesReceivers",
                keyColumns: new[] { "MessageId", "ReceiverId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "MessagesReceivers",
                keyColumns: new[] { "MessageId", "ReceiverId" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "MessagesReceivers",
                keyColumns: new[] { "MessageId", "ReceiverId" },
                keyValues: new object[] { 4, 2 });

            migrationBuilder.DeleteData(
                table: "MessagesReceivers",
                keyColumns: new[] { "MessageId", "ReceiverId" },
                keyValues: new object[] { 6, 2 });

            migrationBuilder.DeleteData(
                table: "MessagesReceivers",
                keyColumns: new[] { "MessageId", "ReceiverId" },
                keyValues: new object[] { 4, 4 });

            migrationBuilder.DeleteData(
                table: "MessagesReceivers",
                keyColumns: new[] { "MessageId", "ReceiverId" },
                keyValues: new object[] { 5, 4 });

            migrationBuilder.DeleteData(
                table: "MessagesReceivers",
                keyColumns: new[] { "MessageId", "ReceiverId" },
                keyValues: new object[] { 6, 4 });

            migrationBuilder.DeleteData(
                table: "MessagesReceivers",
                keyColumns: new[] { "MessageId", "ReceiverId" },
                keyValues: new object[] { 7, 4 });

            migrationBuilder.DeleteData(
                table: "MessagesReceivers",
                keyColumns: new[] { "MessageId", "ReceiverId" },
                keyValues: new object[] { 2, 5 });

            migrationBuilder.DeleteData(
                table: "MessagesReceivers",
                keyColumns: new[] { "MessageId", "ReceiverId" },
                keyValues: new object[] { 5, 5 });

            migrationBuilder.DeleteData(
                table: "MessagesReceivers",
                keyColumns: new[] { "MessageId", "ReceiverId" },
                keyValues: new object[] { 5, 6 });

            migrationBuilder.DeleteData(
                table: "MessagesReceivers",
                keyColumns: new[] { "MessageId", "ReceiverId" },
                keyValues: new object[] { 3, 7 });

            migrationBuilder.DeleteData(
                table: "MessagesReceivers",
                keyColumns: new[] { "MessageId", "ReceiverId" },
                keyValues: new object[] { 7, 7 });

            migrationBuilder.DeleteData(
                table: "MessagesReceivers",
                keyColumns: new[] { "MessageId", "ReceiverId" },
                keyValues: new object[] { 8, 7 });

            migrationBuilder.DeleteData(
                table: "MessagesReceivers",
                keyColumns: new[] { "MessageId", "ReceiverId" },
                keyValues: new object[] { 10, 10 });

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateOfEvent",
                value: new DateTime(2023, 1, 6, 11, 29, 14, 87, DateTimeKind.Utc).AddTicks(5359));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateOfEvent",
                value: new DateTime(2023, 1, 6, 11, 29, 14, 87, DateTimeKind.Utc).AddTicks(5394));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateOfEvent",
                value: new DateTime(2023, 1, 6, 11, 29, 14, 87, DateTimeKind.Utc).AddTicks(5398));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 6,
                column: "DateOfEvent",
                value: new DateTime(2023, 1, 6, 11, 29, 14, 87, DateTimeKind.Utc).AddTicks(5409));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 7,
                column: "DateOfEvent",
                value: new DateTime(2023, 1, 6, 11, 29, 14, 87, DateTimeKind.Utc).AddTicks(5412));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 9,
                column: "DateOfEvent",
                value: new DateTime(2023, 1, 6, 11, 29, 14, 87, DateTimeKind.Utc).AddTicks(5418));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 10,
                column: "DateOfEvent",
                value: new DateTime(2023, 1, 6, 11, 29, 14, 87, DateTimeKind.Utc).AddTicks(5421));
        }
    }
}
