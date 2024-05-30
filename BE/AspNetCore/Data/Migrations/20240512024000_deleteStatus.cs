using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PixelPalette.Data.Migrations
{
    public partial class deleteStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Follower");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Follower",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
