using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PixelPalette.Data.Migrations
{
    public partial class addPostCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "Collection",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "PostCount",
                table: "Collection",
                type: "int",
                nullable: false,
                defaultValueSql: "('0')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "Collection");

            migrationBuilder.DropColumn(
                name: "PostCount",
                table: "Collection");
        }
    }
}
