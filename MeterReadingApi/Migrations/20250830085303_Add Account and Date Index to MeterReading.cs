using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeterReadingApi.Migrations
{
    /// <inheritdoc />
    public partial class AddAccountandDateIndextoMeterReading : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MeterReadings_AccountId",
                table: "MeterReadings");

            migrationBuilder.CreateIndex(
                name: "IX_MeterReadings_AccountId_MeterReadingDateTime",
                table: "MeterReadings",
                columns: new[] { "AccountId", "MeterReadingDateTime" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MeterReadings_AccountId_MeterReadingDateTime",
                table: "MeterReadings");

            migrationBuilder.CreateIndex(
                name: "IX_MeterReadings_AccountId",
                table: "MeterReadings",
                column: "AccountId");
        }
    }
}
