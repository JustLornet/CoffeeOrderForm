using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyTestAppBack.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ChangeStandComposData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "dictStandartCompositions",
                keyColumns: new[] { "CoffeeTypeId", "IngredientId" },
                keyValues: new object[] { 1L, 1L });

            migrationBuilder.DeleteData(
                table: "dictStandartCompositions",
                keyColumns: new[] { "CoffeeTypeId", "IngredientId" },
                keyValues: new object[] { 5L, 1L });

            migrationBuilder.DeleteData(
                table: "dictStandartCompositions",
                keyColumns: new[] { "CoffeeTypeId", "IngredientId" },
                keyValues: new object[] { 5L, 2L });

            migrationBuilder.DeleteData(
                table: "dictStandartCompositions",
                keyColumns: new[] { "CoffeeTypeId", "IngredientId" },
                keyValues: new object[] { 8L, 1L });

            migrationBuilder.DeleteData(
                table: "dictStandartCompositions",
                keyColumns: new[] { "CoffeeTypeId", "IngredientId" },
                keyValues: new object[] { 8L, 3L });

            migrationBuilder.DeleteData(
                table: "dictStandartCompositions",
                keyColumns: new[] { "CoffeeTypeId", "IngredientId" },
                keyValues: new object[] { 8L, 16L });

            migrationBuilder.DeleteData(
                table: "dictStandartCompositions",
                keyColumns: new[] { "CoffeeTypeId", "IngredientId" },
                keyValues: new object[] { 9L, 1L });

            migrationBuilder.DeleteData(
                table: "dictStandartCompositions",
                keyColumns: new[] { "CoffeeTypeId", "IngredientId" },
                keyValues: new object[] { 9L, 3L });

            migrationBuilder.DeleteData(
                table: "dictStandartCompositions",
                keyColumns: new[] { "CoffeeTypeId", "IngredientId" },
                keyValues: new object[] { 9L, 16L });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "dictStandartCompositions",
                columns: new[] { "CoffeeTypeId", "IngredientId", "Value" },
                values: new object[,]
                {
                    { 1L, 1L, 50L },
                    { 5L, 1L, 60L },
                    { 5L, 2L, 120L },
                    { 8L, 1L, 50L },
                    { 8L, 3L, 50L },
                    { 8L, 16L, 50L },
                    { 9L, 1L, 50L },
                    { 9L, 3L, 150L },
                    { 9L, 16L, 50L }
                });
        }
    }
}
