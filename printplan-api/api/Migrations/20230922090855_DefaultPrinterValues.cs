using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace printplan_api.Migrations
{
    /// <inheritdoc />
    public partial class DefaultPrinterValues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Printers",
                columns: new[] { "Id", "Name", "PreheatingDuration", "PrinterSpeed" },
                values: new object[] { 1, "Default Printer", 120f, 10f });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Printers",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
