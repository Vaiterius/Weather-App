using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CountryModel.Migrations
{
    /// <inheritdoc />
    public partial class Removelengthconstraintforcountryname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Country",
                type: "varchar(max)",
                unicode: false,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldUnicode: false,
                oldMaxLength: 20);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Country",
                type: "varchar(20)",
                unicode: false,
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldUnicode: false);
        }
    }
}
