using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace printplan_api.Migrations
{
    /// <inheritdoc />
    public partial class DefaultEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "FilamentSpools",
                columns: new[] { "Id", "Color", "Lenght", "Name", "Quantity" },
                values: new object[,]
                {
                    { 1, "Black", 346, "Bobine_0", 4 },
                    { 2, "Black", 990, "Bobine_1", 1 },
                    { 3, "Black", 831, "Bobine_2", 4 },
                    { 4, "Black", 537, "Bobine_3", 4 },
                    { 5, "Black", 599, "Bobine_4", 6 },
                    { 6, "Black", 334, "Bobine_5", 2 },
                    { 7, "Black", 728, "Bobine_6", 6 },
                    { 8, "Black", 470, "Bobine_7", 5 },
                    { 9, "Black", 333, "Bobine_8", 8 },
                    { 10, "Black", 134, "Bobine_9", 4 }
                });

            migrationBuilder.InsertData(
                table: "PrintModels",
                columns: new[] { "Id", "Name", "RequiredFilamentLenght" },
                values: new object[] { 1, "Super Model", 250f });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FilamentSpools",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "FilamentSpools",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "FilamentSpools",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "FilamentSpools",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "FilamentSpools",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "FilamentSpools",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "FilamentSpools",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "FilamentSpools",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "FilamentSpools",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "FilamentSpools",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "PrintModels",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
