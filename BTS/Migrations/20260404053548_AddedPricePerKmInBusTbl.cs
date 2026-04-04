using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BTS.Migrations
{
    /// <inheritdoc />
    public partial class AddedPricePerKmInBusTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PricePerKm",
                table: "Buses",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PricePerKm",
                table: "Buses");
        }
    }
}
