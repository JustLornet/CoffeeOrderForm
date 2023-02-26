using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyTestAppBack.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CoffeeSyrups",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoffeeSyrups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "dictCoffeeIngredient",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    IsOptional = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dictCoffeeIngredient", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "dictCoffeeType",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dictCoffeeType", x => x.Id);
                });

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
                table: "CoffeeSyrups",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1L, "Карамель" },
                    { 2L, "Амаретто" },
                    { 3L, "Лесной орех" },
                    { 4L, "Шоколад" },
                    { 5L, "Соленая карамель" },
                    { 6L, "Кокос" }
                });

            migrationBuilder.InsertData(
                table: "dictCoffeeIngredient",
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
                table: "dictCoffeeType",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1L, "Как появился этот вид кофе? Во время Второй мировой войны американские солдаты искали в Италии свой традиционный фильтр-кофе, поэтому итальянские бариста решили разбавить эспрессо, в результате чего получился совершенно новый напиток.", "Эспрессо" },
                    { 2L, "Двойная порция эспрессо", "Доппио" },
                    { 3L, "Эспрессо с увеличенным количеством воды", "Лунго" },
                    { 4L, "Многие считают, что в ристретто слишком много кофеина, но это заблуждение. Первые несколько секунд заваривания из кофе выделяются эфирные масла, придающие ему насыщенный вкус, а кофеин поступает в напиток позднее. Из-за этого в ристретто обычно даже меньше кофеина, чем в эспрессо.", "Ристретто" },
                    { 5L, "Эспрессо, разбавленный кипятком в пропорции 1:2 или 1:3", "Американо" },
                    { 6L, "Двойная порция эспрессо, куда добавляют немного горячего молока с пеной", "Флэт уайт" },
                    { 7L, "Слово «макиато» с итальянского переводится как «пятнышко». Такое кофейное темное пятнышко образуется сверху на молочной пене после вливания эспрессо в горячее молоко.", "Макиато" },
                    { 8L, "Капучино в переводе с итальянского означает «капуцинский», так как его придумали монахи-капуцины. Основная составляющая капучино - это эспрессо, вторая – молоко.", "Капучино" },
                    { 9L, "Название итальянского кофе латте в переводе означает «кофе с молоком». Как и следует из названия, основными составляющими этого напитка являются эспрессо и молоко.", "Латте" },
                    { 10L, "Охлаждённый кофе с шариком мороженого. Напиток подают в айриш-бокале с соломинкой", "Глясе" },
                    { 11L, "Коктейль (по сути, пена) из эспрессо со сливками и ванильным сахаром, взбитого в капучинаторе", "Раф" },
                    { 13L, "Во вкусе мокко больше преобладает шоколадно-сливочный аромат, чем кофейный. А температура готового напитка не очень высокая, так как сироп и сливки добавляются в мокко холодными.", "Мокко" },
                    { 14L, "Кофе по-турински, состоящий из 3 слоёв: горячего шоколада на молоке, эспрессо и взбитых сливок. Сверху кофе украшают тёртым горьким шоколадом и мятой", "Бичерин" }
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

            migrationBuilder.CreateIndex(
                name: "IX_CoffeeSyrups_Name",
                table: "CoffeeSyrups",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_dictCoffeeIngredient_Name",
                table: "dictCoffeeIngredient",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_dictCoffeeType_Name",
                table: "dictCoffeeType",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoffeeCompositions");

            migrationBuilder.DropTable(
                name: "CoffeeSyrups");

            migrationBuilder.DropTable(
                name: "dictCoffeeIngredient");

            migrationBuilder.DropTable(
                name: "dictCoffeeType");
        }
    }
}
