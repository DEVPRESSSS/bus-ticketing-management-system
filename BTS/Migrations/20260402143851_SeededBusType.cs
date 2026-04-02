using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BTS.Migrations
{
    /// <inheritdoc />
    public partial class SeededBusType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "BusType",
                columns: new[] { "BusTypeId", "BusTypeName", "CreatedAt" },
                values: new object[,]
                {
                    { "BUSCOMPANY-07d77", "DELUXE", new DateTime(2026, 4, 2, 22, 4, 37, 0, DateTimeKind.Unspecified) },
                    { "BUSCOMPANY-07d78", "ORDINARY/REGULAR", new DateTime(2026, 4, 2, 22, 4, 37, 0, DateTimeKind.Unspecified) },
                    { "BUSCOMPANY-07d79", "AIR CONDITIONED", new DateTime(2026, 4, 2, 22, 4, 37, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BusType",
                keyColumn: "BusTypeId",
                keyValue: "BUSCOMPANY-07d77");

            migrationBuilder.DeleteData(
                table: "BusType",
                keyColumn: "BusTypeId",
                keyValue: "BUSCOMPANY-07d78");

            migrationBuilder.DeleteData(
                table: "BusType",
                keyColumn: "BusTypeId",
                keyValue: "BUSCOMPANY-07d79");
        }
    }
}
