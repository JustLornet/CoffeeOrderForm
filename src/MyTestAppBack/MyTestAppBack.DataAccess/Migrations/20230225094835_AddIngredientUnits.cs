using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyTestAppBack.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddIngredientUnits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoffeeOrders_dictCoffeeSyrups_CoffeeSyrupId",
                table: "CoffeeOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomCompositions_dictCoffeeIngredients_CoffeeIngredientId",
                table: "CustomCompositions");

            migrationBuilder.DropForeignKey(
                name: "FK_dictStandartCompositions_dictCoffeeIngredients_CoffeeIngredientId",
                table: "dictStandartCompositions");

            migrationBuilder.DropTable(
                name: "dictCoffeeIngredients");

            migrationBuilder.DropTable(
                name: "dictCoffeeSyrups");

            migrationBuilder.RenameColumn(
                name: "CoffeeIngredientId",
                table: "dictStandartCompositions",
                newName: "IngredientId");

            migrationBuilder.RenameIndex(
                name: "IX_dictStandartCompositions_CoffeeIngredientId",
                table: "dictStandartCompositions",
                newName: "IX_dictStandartCompositions_IngredientId");

            migrationBuilder.RenameColumn(
                name: "CoffeeIngredientId",
                table: "CustomCompositions",
                newName: "IngredientId");

            migrationBuilder.RenameIndex(
                name: "IX_CustomCompositions_CoffeeIngredientId_Value",
                table: "CustomCompositions",
                newName: "IX_CustomCompositions_IngredientId_Value");

            migrationBuilder.RenameColumn(
                name: "CoffeeSyrupId",
                table: "CoffeeOrders",
                newName: "SyrupId");

            migrationBuilder.RenameIndex(
                name: "IX_CoffeeOrders_CoffeeSyrupId",
                table: "CoffeeOrders",
                newName: "IX_CoffeeOrders_SyrupId");

            migrationBuilder.CreateTable(
                name: "dictIngredientUnits",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dictIngredientUnits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "dictSyrups",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dictSyrups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "dictIngredients",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    IngredientUnitId = table.Column<long>(type: "INTEGER", nullable: false),
                    IsOptional = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dictIngredients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dictIngredients_dictIngredientUnits_IngredientUnitId",
                        column: x => x.IngredientUnitId,
                        principalTable: "dictIngredientUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "dictIngredientUnits",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1L, "милиграмм", "мг" },
                    { 2L, "милилитр", "мл" },
                    { 3L, "литр", "л" }
                });

            migrationBuilder.InsertData(
                table: "dictSyrups",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1L, "Сироп BARINOFF \"Соленая карамель\" обладает аппетитной густой консистенцией и покоряет мягкими нюансами карамели и ванили", "Карамель" },
                    { 2L, "Сироп Амаретто – безалкогольный вариант традиционного миндального ликера, обладающий богатым вкусом, красивым золотистым оттенком", "Амаретто" },
                    { 3L, "Сироп «Лесной орех» имеет желтовато-оливковый оттенок, насыщенный аромат фундука и маслянистый, дымчатый вкус натуральных лесных орехов", "Лесной орех" },
                    { 4L, "Шоколодный сироп для кофе - не канон", "Шоколад" },
                    { 5L, "Сироп «Соленая карамель» обладает сильным, глубоким и ярким ароматом солоноватой карамели", "Соленая карамель" },
                    { 6L, "Мммм (^^)", "Кокос" }
                });

            migrationBuilder.InsertData(
                table: "dictIngredients",
                columns: new[] { "Id", "IngredientUnitId", "IsOptional", "Name" },
                values: new object[,]
                {
                    { 1L, 2L, false, "Эспрессо" },
                    { 2L, 2L, false, "Вода" },
                    { 3L, 2L, false, "Молоко" },
                    { 4L, 2L, true, "Сливки" },
                    { 5L, 1L, true, "Сахар" },
                    { 6L, 2L, true, "Взбитые сливки" },
                    { 7L, 1L, true, "Мороженое" },
                    { 8L, 1L, true, "Тертый шоколад" },
                    { 9L, 1L, true, "Лимон" },
                    { 10L, 1L, true, "Молотый орех" },
                    { 11L, 3L, true, "Виски" },
                    { 12L, 1L, true, "Мед" },
                    { 13L, 3L, true, "Коньяк" },
                    { 14L, 3L, true, "Ликер" },
                    { 15L, 1L, true, "Корица" },
                    { 16L, 2L, false, "Вспененное молоко" },
                    { 17L, 1L, true, "Карамель" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_dictIngredients_IngredientUnitId",
                table: "dictIngredients",
                column: "IngredientUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_dictIngredients_Name",
                table: "dictIngredients",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_dictIngredientUnits_Name",
                table: "dictIngredientUnits",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_dictSyrups_Name",
                table: "dictSyrups",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CoffeeOrders_dictSyrups_SyrupId",
                table: "CoffeeOrders",
                column: "SyrupId",
                principalTable: "dictSyrups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomCompositions_dictIngredients_IngredientId",
                table: "CustomCompositions",
                column: "IngredientId",
                principalTable: "dictIngredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_dictStandartCompositions_dictIngredients_IngredientId",
                table: "dictStandartCompositions",
                column: "IngredientId",
                principalTable: "dictIngredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoffeeOrders_dictSyrups_SyrupId",
                table: "CoffeeOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomCompositions_dictIngredients_IngredientId",
                table: "CustomCompositions");

            migrationBuilder.DropForeignKey(
                name: "FK_dictStandartCompositions_dictIngredients_IngredientId",
                table: "dictStandartCompositions");

            migrationBuilder.DropTable(
                name: "dictIngredients");

            migrationBuilder.DropTable(
                name: "dictSyrups");

            migrationBuilder.DropTable(
                name: "dictIngredientUnits");

            migrationBuilder.RenameColumn(
                name: "IngredientId",
                table: "dictStandartCompositions",
                newName: "CoffeeIngredientId");

            migrationBuilder.RenameIndex(
                name: "IX_dictStandartCompositions_IngredientId",
                table: "dictStandartCompositions",
                newName: "IX_dictStandartCompositions_CoffeeIngredientId");

            migrationBuilder.RenameColumn(
                name: "IngredientId",
                table: "CustomCompositions",
                newName: "CoffeeIngredientId");

            migrationBuilder.RenameIndex(
                name: "IX_CustomCompositions_IngredientId_Value",
                table: "CustomCompositions",
                newName: "IX_CustomCompositions_CoffeeIngredientId_Value");

            migrationBuilder.RenameColumn(
                name: "SyrupId",
                table: "CoffeeOrders",
                newName: "CoffeeSyrupId");

            migrationBuilder.RenameIndex(
                name: "IX_CoffeeOrders_SyrupId",
                table: "CoffeeOrders",
                newName: "IX_CoffeeOrders_CoffeeSyrupId");

            migrationBuilder.CreateTable(
                name: "dictCoffeeIngredients",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IsOptional = table.Column<bool>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dictCoffeeIngredients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "dictCoffeeSyrups",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dictCoffeeSyrups", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "dictCoffeeIngredients",
                columns: new[] { "Id", "IsOptional", "Name" },
                values: new object[,]
                {
                    { 1L, false, "Эспрессо" },
                    { 2L, false, "Вода" },
                    { 3L, false, "Молоко" },
                    { 4L, true, "Сливки" },
                    { 5L, true, "Сахар" },
                    { 6L, true, "Взбитые сливки" },
                    { 7L, true, "Мороженое" },
                    { 8L, true, "Тертый шоколад" },
                    { 9L, true, "Лимон" },
                    { 10L, true, "Молотый орех" },
                    { 11L, true, "Виски" },
                    { 12L, true, "Мед" },
                    { 13L, true, "Коньяк" },
                    { 14L, true, "Ликер" },
                    { 15L, true, "Корица" },
                    { 16L, false, "Вспененное молоко" },
                    { 17L, true, "Карамель" }
                });

            migrationBuilder.InsertData(
                table: "dictCoffeeSyrups",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1L, "Сироп BARINOFF \"Соленая карамель\" обладает аппетитной густой консистенцией и покоряет мягкими нюансами карамели и ванили", "Карамель" },
                    { 2L, "Сироп Амаретто – безалкогольный вариант традиционного миндального ликера, обладающий богатым вкусом, красивым золотистым оттенком", "Амаретто" },
                    { 3L, "Сироп «Лесной орех» имеет желтовато-оливковый оттенок, насыщенный аромат фундука и маслянистый, дымчатый вкус натуральных лесных орехов", "Лесной орех" },
                    { 4L, "Шоколодный сироп для кофе - не канон", "Шоколад" },
                    { 5L, "Сироп «Соленая карамель» обладает сильным, глубоким и ярким ароматом солоноватой карамели", "Соленая карамель" },
                    { 6L, "Мммм (^^)", "Кокос" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_dictCoffeeIngredients_Name",
                table: "dictCoffeeIngredients",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_dictCoffeeSyrups_Name",
                table: "dictCoffeeSyrups",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CoffeeOrders_dictCoffeeSyrups_CoffeeSyrupId",
                table: "CoffeeOrders",
                column: "CoffeeSyrupId",
                principalTable: "dictCoffeeSyrups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomCompositions_dictCoffeeIngredients_CoffeeIngredientId",
                table: "CustomCompositions",
                column: "CoffeeIngredientId",
                principalTable: "dictCoffeeIngredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_dictStandartCompositions_dictCoffeeIngredients_CoffeeIngredientId",
                table: "dictStandartCompositions",
                column: "CoffeeIngredientId",
                principalTable: "dictCoffeeIngredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
