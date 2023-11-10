using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace printplan_api.Migrations
{
    /// <inheritdoc />
    public partial class MoreModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "FilamentSpools",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Lenght", "Quantity" },
                values: new object[] { 363, 1 });

            migrationBuilder.UpdateData(
                table: "FilamentSpools",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Lenght", "Quantity" },
                values: new object[] { 513, 9 });

            migrationBuilder.UpdateData(
                table: "FilamentSpools",
                keyColumn: "Id",
                keyValue: 3,
                column: "Lenght",
                value: 623);

            migrationBuilder.UpdateData(
                table: "FilamentSpools",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Lenght", "Quantity" },
                values: new object[] { 515, 5 });

            migrationBuilder.UpdateData(
                table: "FilamentSpools",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Lenght", "Quantity" },
                values: new object[] { 608, 5 });

            migrationBuilder.UpdateData(
                table: "FilamentSpools",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Lenght", "Quantity" },
                values: new object[] { 498, 3 });

            migrationBuilder.UpdateData(
                table: "FilamentSpools",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Lenght", "Quantity" },
                values: new object[] { 234, 8 });

            migrationBuilder.UpdateData(
                table: "FilamentSpools",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Lenght", "Quantity" },
                values: new object[] { 995, 2 });

            migrationBuilder.UpdateData(
                table: "FilamentSpools",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Lenght", "Quantity" },
                values: new object[] { 975, 1 });

            migrationBuilder.UpdateData(
                table: "FilamentSpools",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Lenght", "Quantity" },
                values: new object[] { 115, 5 });

            migrationBuilder.UpdateData(
                table: "PrintModels",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Name", "RequiredFilamentLenght" },
                values: new object[] { "Petit model", 5f });

            migrationBuilder.InsertData(
                table: "PrintModels",
                columns: new[] { "Id", "Name", "RequiredFilamentLenght" },
                values: new object[,]
                {
                    { 2, "Moyen model", 10f },
                    { 3, "Grand model", 30f },
                    { 4, "Gargantua", 1000f }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PrintModels",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PrintModels",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PrintModels",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "FilamentSpools",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Lenght", "Quantity" },
                values: new object[] { 346, 4 });

            migrationBuilder.UpdateData(
                table: "FilamentSpools",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Lenght", "Quantity" },
                values: new object[] { 990, 1 });

            migrationBuilder.UpdateData(
                table: "FilamentSpools",
                keyColumn: "Id",
                keyValue: 3,
                column: "Lenght",
                value: 831);

            migrationBuilder.UpdateData(
                table: "FilamentSpools",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Lenght", "Quantity" },
                values: new object[] { 537, 4 });

            migrationBuilder.UpdateData(
                table: "FilamentSpools",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Lenght", "Quantity" },
                values: new object[] { 599, 6 });

            migrationBuilder.UpdateData(
                table: "FilamentSpools",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Lenght", "Quantity" },
                values: new object[] { 334, 2 });

            migrationBuilder.UpdateData(
                table: "FilamentSpools",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Lenght", "Quantity" },
                values: new object[] { 728, 6 });

            migrationBuilder.UpdateData(
                table: "FilamentSpools",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Lenght", "Quantity" },
                values: new object[] { 470, 5 });

            migrationBuilder.UpdateData(
                table: "FilamentSpools",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Lenght", "Quantity" },
                values: new object[] { 333, 8 });

            migrationBuilder.UpdateData(
                table: "FilamentSpools",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Lenght", "Quantity" },
                values: new object[] { 134, 4 });

            migrationBuilder.UpdateData(
                table: "PrintModels",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Name", "RequiredFilamentLenght" },
                values: new object[] { "Super Model", 250f });
        }
    }
}
