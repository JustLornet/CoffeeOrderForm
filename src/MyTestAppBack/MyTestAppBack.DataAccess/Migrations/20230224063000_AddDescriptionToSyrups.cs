using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyTestAppBack.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddDescriptionToSyrups : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "CoffeeSyrups",
                type: "TEXT",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "CoffeeSyrups",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Description",
                value: "Сироп BARINOFF \"Соленая карамель\" обладает аппетитной густой консистенцией и покоряет мягкими нюансами карамели и ванили");

            migrationBuilder.UpdateData(
                table: "CoffeeSyrups",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Description",
                value: "Сироп Амаретто – безалкогольный вариант традиционного миндального ликера, обладающий богатым вкусом, красивым золотистым оттенком");

            migrationBuilder.UpdateData(
                table: "CoffeeSyrups",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Description",
                value: "Сироп «Лесной орех» имеет желтовато-оливковый оттенок, насыщенный аромат фундука и маслянистый, дымчатый вкус натуральных лесных орехов");

            migrationBuilder.UpdateData(
                table: "CoffeeSyrups",
                keyColumn: "Id",
                keyValue: 4L,
                column: "Description",
                value: "Шоколодный сироп для кофе - не канон");

            migrationBuilder.UpdateData(
                table: "CoffeeSyrups",
                keyColumn: "Id",
                keyValue: 5L,
                column: "Description",
                value: "Сироп «Соленая карамель» обладает сильным, глубоким и ярким ароматом солоноватой карамели");

            migrationBuilder.UpdateData(
                table: "CoffeeSyrups",
                keyColumn: "Id",
                keyValue: 6L,
                column: "Description",
                value: "Мммм (^^)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "CoffeeSyrups");
        }
    }
}
