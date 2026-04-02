using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BTS.Migrations
{
    /// <inheritdoc />
    public partial class SeedBusCompantTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "BusCompanies",
                columns: new[] { "BusCompanyId", "Address", "CompanyName", "ContactNumber", "CreatedAt", "Email" },
                values: new object[,]
                {
                    { "BUSCOMPANY-07d79", "Manila, Recto", "BALIWAG", "09488749263", new DateTime(2026, 4, 2, 22, 4, 37, 0, DateTimeKind.Unspecified), "incs@gmail.com" },
                    { "BUSCOMPANY-37995", "Manila, Recto Station", "INC", "09488549263", new DateTime(2026, 4, 2, 22, 4, 37, 0, DateTimeKind.Unspecified), "inc@gmail.com" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BusCompanies",
                keyColumn: "BusCompanyId",
                keyValue: "BUSCOMPANY-07d79");

            migrationBuilder.DeleteData(
                table: "BusCompanies",
                keyColumn: "BusCompanyId",
                keyValue: "BUSCOMPANY-37995");
        }
    }
}
