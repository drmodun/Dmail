using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dmail.Data.Migrations
{
    public partial class Seed2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "Id", "Body", "CreatedAt", "DateOfEvent", "IsEvent", "SenderId", "Title" },
                values: new object[] { 1, "Pomoc pls", new DateTime(2020, 8, 31, 22, 0, 0, 0, DateTimeKind.Utc), new DateTime(2023, 1, 6, 2, 37, 54, 836, DateTimeKind.Utc).AddTicks(1887), false, 1, "Pomoc" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "_password" },
                values: new object[,]
                {
                    { 3, "Marko@markovi.markic", "marko" },
                    { 4, "Duje@dump.hr", "Kick" },
                    { 5, "Janko@gmail.com", "janv1" },
                    { 6, "bart@dump.hr", "bartV10" },
                    { 7, "Mao@yahoo.com", "mao" },
                    { 8, "Fake@fakeemail.fakecountry", "Fake" },
                    { 9, "Empty@empty.empty", "Empty" },
                    { 10, "User@adress.domain", "Password" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
