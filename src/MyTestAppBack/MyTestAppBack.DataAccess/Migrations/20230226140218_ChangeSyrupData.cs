using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyTestAppBack.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ChangeSyrupData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "dictSyrups",
                keyColumn: "Id",
                keyValue: 6L,
                column: "Description",
                value: "Кокосовый сироп — это концентрированный сладкий продукт с ароматом знаменитого тропического фрукта, который способен подарить очарование любому напитку");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "dictSyrups",
                keyColumn: "Id",
                keyValue: 6L,
                column: "Description",
                value: "Мммм (^^)");
        }
    }
}
