using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyTestAppBack.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderCreatRealization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoffeeCompositions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_dictCoffeeType",
                table: "dictCoffeeType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_dictCoffeeIngredient",
                table: "dictCoffeeIngredient");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CoffeeSyrups",
                table: "CoffeeSyrups");

            migrationBuilder.RenameTable(
                name: "dictCoffeeType",
                newName: "dictCoffeeTypes");

            migrationBuilder.RenameTable(
                name: "dictCoffeeIngredient",
                newName: "dictCoffeeIngredients");

            migrationBuilder.RenameTable(
                name: "CoffeeSyrups",
                newName: "dictCoffeeSyrups");

            migrationBuilder.RenameIndex(
                name: "IX_dictCoffeeType_Name",
                table: "dictCoffeeTypes",
                newName: "IX_dictCoffeeTypes_Name");

            migrationBuilder.RenameIndex(
                name: "IX_dictCoffeeIngredient_Name",
                table: "dictCoffeeIngredients",
                newName: "IX_dictCoffeeIngredients_Name");

            migrationBuilder.RenameIndex(
                name: "IX_CoffeeSyrups_Name",
                table: "dictCoffeeSyrups",
                newName: "IX_dictCoffeeSyrups_Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_dictCoffeeTypes",
                table: "dictCoffeeTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_dictCoffeeIngredients",
                table: "dictCoffeeIngredients",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_dictCoffeeSyrups",
                table: "dictCoffeeSyrups",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CoffeeOrders",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CoffeeTypeId = table.Column<long>(type: "INTEGER", nullable: false),
                    CustomerName = table.Column<string>(type: "TEXT", nullable: false),
                    Comments = table.Column<string>(type: "TEXT", nullable: true),
                    OrderDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    OrderExecutionDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CoffeeSyrupId = table.Column<long>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoffeeOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoffeeOrders_dictCoffeeSyrups_CoffeeSyrupId",
                        column: x => x.CoffeeSyrupId,
                        principalTable: "dictCoffeeSyrups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CoffeeOrders_dictCoffeeTypes_CoffeeTypeId",
                        column: x => x.CoffeeTypeId,
                        principalTable: "dictCoffeeTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomCompositions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CoffeeIngredientId = table.Column<long>(type: "INTEGER", nullable: false),
                    Value = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomCompositions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomCompositions_dictCoffeeIngredients_CoffeeIngredientId",
                        column: x => x.CoffeeIngredientId,
                        principalTable: "dictCoffeeIngredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "dictStandartCompositions",
                columns: table => new
                {
                    CoffeeTypeId = table.Column<long>(type: "INTEGER", nullable: false),
                    CoffeeIngredientId = table.Column<long>(type: "INTEGER", nullable: false),
                    Value = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dictStandartCompositions", x => new { x.CoffeeTypeId, x.CoffeeIngredientId });
                    table.ForeignKey(
                        name: "FK_dictStandartCompositions_dictCoffeeIngredients_CoffeeIngredientId",
                        column: x => x.CoffeeIngredientId,
                        principalTable: "dictCoffeeIngredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dictStandartCompositions_dictCoffeeTypes_CoffeeTypeId",
                        column: x => x.CoffeeTypeId,
                        principalTable: "dictCoffeeTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderCustomCompositions",
                columns: table => new
                {
                    CoffeeOrdersId = table.Column<long>(type: "INTEGER", nullable: false),
                    CustomCompositionsId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderCustomCompositions", x => new { x.CoffeeOrdersId, x.CustomCompositionsId });
                    table.ForeignKey(
                        name: "FK_OrderCustomCompositions_CoffeeOrders_CoffeeOrdersId",
                        column: x => x.CoffeeOrdersId,
                        principalTable: "CoffeeOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderCustomCompositions_CustomCompositions_CustomCompositionsId",
                        column: x => x.CustomCompositionsId,
                        principalTable: "CustomCompositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "dictStandartCompositions",
                columns: new[] { "CoffeeIngredientId", "CoffeeTypeId", "Value" },
                values: new object[,]
                {
                    { 1L, 1L, 50L },
                    { 1L, 5L, 60L },
                    { 2L, 5L, 120L },
                    { 1L, 8L, 50L },
                    { 3L, 8L, 50L },
                    { 16L, 8L, 50L },
                    { 1L, 9L, 50L },
                    { 3L, 9L, 150L },
                    { 16L, 9L, 50L }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CoffeeOrders_CoffeeSyrupId",
                table: "CoffeeOrders",
                column: "CoffeeSyrupId");

            migrationBuilder.CreateIndex(
                name: "IX_CoffeeOrders_CoffeeTypeId",
                table: "CoffeeOrders",
                column: "CoffeeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomCompositions_CoffeeIngredientId_Value",
                table: "CustomCompositions",
                columns: new[] { "CoffeeIngredientId", "Value" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_dictStandartCompositions_CoffeeIngredientId",
                table: "dictStandartCompositions",
                column: "CoffeeIngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderCustomCompositions_CustomCompositionsId",
                table: "OrderCustomCompositions",
                column: "CustomCompositionsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "dictStandartCompositions");

            migrationBuilder.DropTable(
                name: "OrderCustomCompositions");

            migrationBuilder.DropTable(
                name: "CoffeeOrders");

            migrationBuilder.DropTable(
                name: "CustomCompositions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_dictCoffeeTypes",
                table: "dictCoffeeTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_dictCoffeeSyrups",
                table: "dictCoffeeSyrups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_dictCoffeeIngredients",
                table: "dictCoffeeIngredients");

            migrationBuilder.RenameTable(
                name: "dictCoffeeTypes",
                newName: "dictCoffeeType");

            migrationBuilder.RenameTable(
                name: "dictCoffeeSyrups",
                newName: "CoffeeSyrups");

            migrationBuilder.RenameTable(
                name: "dictCoffeeIngredients",
                newName: "dictCoffeeIngredient");

            migrationBuilder.RenameIndex(
                name: "IX_dictCoffeeTypes_Name",
                table: "dictCoffeeType",
                newName: "IX_dictCoffeeType_Name");

            migrationBuilder.RenameIndex(
                name: "IX_dictCoffeeSyrups_Name",
                table: "CoffeeSyrups",
                newName: "IX_CoffeeSyrups_Name");

            migrationBuilder.RenameIndex(
                name: "IX_dictCoffeeIngredients_Name",
                table: "dictCoffeeIngredient",
                newName: "IX_dictCoffeeIngredient_Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_dictCoffeeType",
                table: "dictCoffeeType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CoffeeSyrups",
                table: "CoffeeSyrups",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_dictCoffeeIngredient",
                table: "dictCoffeeIngredient",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CoffeeCompositions",
                columns: table => new
                {
                    CoffeeTypeId = table.Column<long>(type: "INTEGER", nullable: false),
                    CoffeeIngredientId = table.Column<long>(type: "INTEGER", nullable: false),
                    Value = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoffeeCompositions", x => new { x.CoffeeTypeId, x.CoffeeIngredientId });
                    table.ForeignKey(
                        name: "FK_CoffeeCompositions_dictCoffeeIngredient_CoffeeIngredientId",
                        column: x => x.CoffeeIngredientId,
                        principalTable: "dictCoffeeIngredient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoffeeCompositions_dictCoffeeType_CoffeeTypeId",
                        column: x => x.CoffeeTypeId,
                        principalTable: "dictCoffeeType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CoffeeCompositions",
                columns: new[] { "CoffeeIngredientId", "CoffeeTypeId", "Value" },
                values: new object[,]
                {
                    { 1L, 1L, 50L },
                    { 1L, 5L, 60L },
                    { 2L, 5L, 120L },
                    { 1L, 8L, 50L },
                    { 3L, 8L, 50L },
                    { 16L, 8L, 50L },
                    { 1L, 9L, 50L },
                    { 3L, 9L, 150L },
                    { 16L, 9L, 50L }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CoffeeCompositions_CoffeeIngredientId",
                table: "CoffeeCompositions",
                column: "CoffeeIngredientId");
        }
    }
}
