using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CountryModel.Migrations
{
    /// <inheritdoc />
    public partial class AddCityNameAndPop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "City",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Population",
                table: "City",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "City");

            migrationBuilder.DropColumn(
                name: "Population",
                table: "City");
        }
    }
}
