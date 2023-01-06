using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dmail.Data.Migrations
{
    public partial class Seed3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateOfEvent",
                value: new DateTime(2023, 1, 6, 11, 29, 14, 87, DateTimeKind.Utc).AddTicks(5359));

            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "Id", "Body", "CreatedAt", "DateOfEvent", "IsEvent", "SenderId", "Title" },
                values: new object[,]
                {
                    { 2, "E mos mi kupit miljeko zaboravia san", new DateTime(2022, 8, 31, 22, 0, 0, 0, DateTimeKind.Utc), new DateTime(2023, 1, 6, 11, 29, 14, 87, DateTimeKind.Utc).AddTicks(5394), false, 3, "Kupovina" },
                    { 3, "Čestitamo osvojili ste besplatni Iphone 14 da prmiite nagradu samo nam dajte vaš matični broj, oib, pin kartice, sve brojeve vezane uz karticu, adresu, legalno ime...", new DateTime(2021, 3, 23, 23, 0, 0, 0, DateTimeKind.Utc), new DateTime(2023, 1, 6, 11, 29, 14, 87, DateTimeKind.Utc).AddTicks(5398), false, 8, "Nagrada" },
                    { 4, "", new DateTime(2021, 8, 31, 22, 0, 0, 0, DateTimeKind.Utc), new DateTime(2023, 1, 30, 23, 0, 0, 0, DateTimeKind.Utc), true, 5, "JanVsJan" },
                    { 5, "", new DateTime(2023, 1, 1, 23, 0, 0, 0, DateTimeKind.Utc), new DateTime(2023, 1, 14, 18, 0, 0, 0, DateTimeKind.Utc), true, 2, "Dump predavanje 8" },
                    { 6, "Hello I would like to apply to dump internship, I will also send you my resume", new DateTime(2022, 12, 11, 23, 0, 0, 0, DateTimeKind.Utc), new DateTime(2023, 1, 6, 11, 29, 14, 87, DateTimeKind.Utc).AddTicks(5409), false, 7, "Job application" },
                    { 7, "Resume: I have succesfully openned visual studio once", new DateTime(2022, 12, 12, 23, 0, 0, 0, DateTimeKind.Utc), new DateTime(2023, 1, 6, 11, 29, 14, 87, DateTimeKind.Utc).AddTicks(5412), false, 6, "Resume" },
                    { 8, "", new DateTime(2020, 8, 31, 22, 0, 0, 0, DateTimeKind.Utc), new DateTime(2023, 1, 12, 23, 0, 0, 0, DateTimeKind.Utc), false, 4, "Job Interview" },
                    { 9, "S obziron na pad kvalitete tvohij domaćih Jane, moram te nažalost obavijestiti da smo došli do odluke da te izbacimo s dump internshipa. Možeš još pratiti predavanja ali nećeš moći sudjelovati u Ic cupu i više ti se neće moći pregledavati domaći.", new DateTime(2022, 12, 29, 23, 0, 0, 0, DateTimeKind.Utc), new DateTime(2023, 1, 6, 11, 29, 14, 87, DateTimeKind.Utc).AddTicks(5418), false, 4, "Obavijest o kicku" },
                    { 10, "Wow can you send emails to yourself thats cool", new DateTime(2023, 1, 1, 23, 0, 0, 0, DateTimeKind.Utc), new DateTime(2023, 1, 6, 11, 29, 14, 87, DateTimeKind.Utc).AddTicks(5421), false, 10, "Help" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateOfEvent",
                value: new DateTime(2023, 1, 6, 2, 37, 54, 836, DateTimeKind.Utc).AddTicks(1887));
        }
    }
}
