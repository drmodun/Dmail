using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dmail.Data.Migrations
{
    public partial class attemt3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:event_answer", "none,accepted,rejected");

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateOfEvent",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateOfEvent",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateOfEvent",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 6,
                column: "DateOfEvent",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 7,
                column: "DateOfEvent",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 9,
                column: "DateOfEvent",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 10,
                column: "DateOfEvent",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .OldAnnotation("Npgsql:Enum:event_answer", "none,accepted,rejected");

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateOfEvent",
                value: new DateTime(2023, 1, 6, 21, 29, 9, 386, DateTimeKind.Utc).AddTicks(750));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateOfEvent",
                value: new DateTime(2023, 1, 6, 21, 29, 9, 386, DateTimeKind.Utc).AddTicks(784));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateOfEvent",
                value: new DateTime(2023, 1, 6, 21, 29, 9, 386, DateTimeKind.Utc).AddTicks(788));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 6,
                column: "DateOfEvent",
                value: new DateTime(2023, 1, 6, 21, 29, 9, 386, DateTimeKind.Utc).AddTicks(800));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 7,
                column: "DateOfEvent",
                value: new DateTime(2023, 1, 6, 21, 29, 9, 386, DateTimeKind.Utc).AddTicks(804));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 9,
                column: "DateOfEvent",
                value: new DateTime(2023, 1, 6, 21, 29, 9, 386, DateTimeKind.Utc).AddTicks(810));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 10,
                column: "DateOfEvent",
                value: new DateTime(2023, 1, 6, 21, 29, 9, 386, DateTimeKind.Utc).AddTicks(813));
        }
    }
}
