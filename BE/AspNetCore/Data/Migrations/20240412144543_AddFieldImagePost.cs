using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PixelPalette.Data.Migrations
{
    public partial class AddFieldImagePost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reference",
                table: "Post");

            migrationBuilder.RenameColumn(
                name: "Media",
                table: "Post",
                newName: "Link");

            migrationBuilder.AlterColumn<string>(
                name: "Detail",
                table: "Post",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "ThumbnailId",
                table: "Post",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ThumbnailUrl",
                table: "Post",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThumbnailId",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "ThumbnailUrl",
                table: "Post");

            migrationBuilder.RenameColumn(
                name: "Link",
                table: "Post",
                newName: "Media");

            migrationBuilder.AlterColumn<string>(
                name: "Detail",
                table: "Post",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Reference",
                table: "Post",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }
    }
}
