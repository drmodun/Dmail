using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dmail.Data.Migrations
{
    public partial class CHangedDefaultBooleanValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                column: "DateOfEvent",
                value: new DateTime(2023, 1, 6, 11, 48, 0, 215, DateTimeKind.Utc).AddTicks(6829));

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
