using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dmail.Data.Migrations
{
    public partial class FinalSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateOfEvent",
                value: new DateTime(2023, 1, 6, 12, 26, 38, 253, DateTimeKind.Utc).AddTicks(881));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateOfEvent",
                value: new DateTime(2023, 1, 6, 12, 26, 38, 253, DateTimeKind.Utc).AddTicks(919));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateOfEvent",
                value: new DateTime(2023, 1, 6, 12, 26, 38, 253, DateTimeKind.Utc).AddTicks(923));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 6,
                column: "DateOfEvent",
                value: new DateTime(2023, 1, 6, 12, 26, 38, 253, DateTimeKind.Utc).AddTicks(935));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Body", "DateOfEvent" },
                values: new object[] { "Resume: I have succesfully opened visual studio once", new DateTime(2023, 1, 6, 12, 26, 38, 253, DateTimeKind.Utc).AddTicks(937) });

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 9,
                column: "DateOfEvent",
                value: new DateTime(2023, 1, 6, 12, 26, 38, 253, DateTimeKind.Utc).AddTicks(944));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 10,
                column: "DateOfEvent",
                value: new DateTime(2023, 1, 6, 12, 26, 38, 253, DateTimeKind.Utc).AddTicks(947));

            migrationBuilder.InsertData(
                table: "Spam",
                columns: new[] { "Blocked", "BlockerId" },
                values: new object[,]
                {
                    { 5, 1 },
                    { 1, 2 },
                    { 5, 2 },
                    { 7, 2 },
                    { 8, 2 },
                    { 5, 4 },
                    { 8, 4 },
                    { 2, 5 },
                    { 2, 6 },
                    { 8, 7 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Spam",
                keyColumns: new[] { "Blocked", "BlockerId" },
                keyValues: new object[] { 5, 1 });

            migrationBuilder.DeleteData(
                table: "Spam",
                keyColumns: new[] { "Blocked", "BlockerId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "Spam",
                keyColumns: new[] { "Blocked", "BlockerId" },
                keyValues: new object[] { 5, 2 });

            migrationBuilder.DeleteData(
                table: "Spam",
                keyColumns: new[] { "Blocked", "BlockerId" },
                keyValues: new object[] { 7, 2 });

            migrationBuilder.DeleteData(
                table: "Spam",
                keyColumns: new[] { "Blocked", "BlockerId" },
                keyValues: new object[] { 8, 2 });

            migrationBuilder.DeleteData(
                table: "Spam",
                keyColumns: new[] { "Blocked", "BlockerId" },
                keyValues: new object[] { 5, 4 });

            migrationBuilder.DeleteData(
                table: "Spam",
                keyColumns: new[] { "Blocked", "BlockerId" },
                keyValues: new object[] { 8, 4 });

            migrationBuilder.DeleteData(
                table: "Spam",
                keyColumns: new[] { "Blocked", "BlockerId" },
                keyValues: new object[] { 2, 5 });

            migrationBuilder.DeleteData(
                table: "Spam",
                keyColumns: new[] { "Blocked", "BlockerId" },
                keyValues: new object[] { 2, 6 });

            migrationBuilder.DeleteData(
                table: "Spam",
                keyColumns: new[] { "Blocked", "BlockerId" },
                keyValues: new object[] { 8, 7 });

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateOfEvent",
                value: new DateTime(2023, 1, 6, 11, 48, 0, 215, DateTimeKind.Utc).AddTicks(6780));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateOfEvent",
                value: new DateTime(2023, 1, 6, 11, 48, 0, 215, DateTimeKind.Utc).AddTicks(6812));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateOfEvent",
                value: new DateTime(2023, 1, 6, 11, 48, 0, 215, DateTimeKind.Utc).AddTicks(6816));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 6,
                column: "DateOfEvent",
                value: new DateTime(2023, 1, 6, 11, 48, 0, 215, DateTimeKind.Utc).AddTicks(6827));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Body", "DateOfEvent" },
                values: new object[] { "Resume: I have succesfully openned visual studio once", new DateTime(2023, 1, 6, 11, 48, 0, 215, DateTimeKind.Utc).AddTicks(6829) });

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 9,
                column: "DateOfEvent",
                value: new DateTime(2023, 1, 6, 11, 48, 0, 215, DateTimeKind.Utc).AddTicks(6835));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 10,
                column: "DateOfEvent",
                value: new DateTime(2023, 1, 6, 11, 48, 0, 215, DateTimeKind.Utc).AddTicks(6838));
        }
    }
}
