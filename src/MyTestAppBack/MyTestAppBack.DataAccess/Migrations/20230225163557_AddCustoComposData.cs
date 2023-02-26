using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyTestAppBack.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddCustoComposData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrderDateTime",
                table: "CoffeeOrders",
                newName: "OrderCreationDateTime");

            migrationBuilder.InsertData(
                table: "CustomCompositions",
                columns: new[] { "Id", "IngredientId", "Value" },
                values: new object[,]
                {
                    { 1L, 1L, 1L },
                    { 2L, 2L, 2L }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CustomCompositions",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "CustomCompositions",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.RenameColumn(
                name: "OrderCreationDateTime",
                table: "CoffeeOrders",
                newName: "OrderDateTime");
        }
    }
}
