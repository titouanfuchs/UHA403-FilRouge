using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace printplan_api.Migrations
{
    /// <inheritdoc />
    public partial class RemovedRandom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.UpdateData(
                table: "FilamentSpools",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Lenght", "Name", "Quantity" },
                values: new object[] { 100, "Bobine_Courte", 2 });

            migrationBuilder.UpdateData(
                table: "FilamentSpools",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Lenght", "Name", "Quantity" },
                values: new object[] { 500, "Bobine_Moyenne", 2 });

            migrationBuilder.UpdateData(
                table: "FilamentSpools",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Lenght", "Name", "Quantity" },
                values: new object[] { 2000, "Bobine_Longue", 2 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "FilamentSpools",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Lenght", "Name", "Quantity" },
                values: new object[] { 363, "Bobine_0", 1 });

            migrationBuilder.UpdateData(
                table: "FilamentSpools",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Lenght", "Name", "Quantity" },
                values: new object[] { 513, "Bobine_1", 9 });

            migrationBuilder.UpdateData(
                table: "FilamentSpools",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Lenght", "Name", "Quantity" },
                values: new object[] { 623, "Bobine_2", 4 });

            migrationBuilder.InsertData(
                table: "FilamentSpools",
                columns: new[] { "Id", "Color", "Lenght", "Name", "Quantity" },
                values: new object[,]
                {
                    { 4, "Black", 515, "Bobine_3", 5 },
                    { 5, "Black", 608, "Bobine_4", 5 },
                    { 6, "Black", 498, "Bobine_5", 3 },
                    { 7, "Black", 234, "Bobine_6", 8 },
                    { 8, "Black", 995, "Bobine_7", 2 },
                    { 9, "Black", 975, "Bobine_8", 1 },
                    { 10, "Black", 115, "Bobine_9", 5 }
                });
        }
    }
}
