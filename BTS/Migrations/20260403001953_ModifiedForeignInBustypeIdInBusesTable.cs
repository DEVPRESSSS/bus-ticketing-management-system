using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BTS.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedForeignInBustypeIdInBusesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buses_BusType_BusId",
                table: "Buses");

            migrationBuilder.AlterColumn<string>(
                name: "BusTypeId",
                table: "Buses",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Buses_BusTypeId",
                table: "Buses",
                column: "BusTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Buses_BusType_BusTypeId",
                table: "Buses",
                column: "BusTypeId",
                principalTable: "BusType",
                principalColumn: "BusTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buses_BusType_BusTypeId",
                table: "Buses");

            migrationBuilder.DropIndex(
                name: "IX_Buses_BusTypeId",
                table: "Buses");

            migrationBuilder.AlterColumn<string>(
                name: "BusTypeId",
                table: "Buses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Buses_BusType_BusId",
                table: "Buses",
                column: "BusId",
                principalTable: "BusType",
                principalColumn: "BusTypeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
