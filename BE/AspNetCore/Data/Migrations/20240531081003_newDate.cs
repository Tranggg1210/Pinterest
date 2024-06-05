using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PixelPalette.Data.Migrations
{
    public partial class newDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Analysises",
                table: "Analysises");

            migrationBuilder.RenameTable(
                name: "Analysises",
                newName: "Analysis");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Analysis",
                type: "datetime",
                nullable: false,
                defaultValueSql: "('0001-01-01T00:00:00.000')",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Analysis",
                table: "Analysis",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Analysis",
                table: "Analysis");

            migrationBuilder.RenameTable(
                name: "Analysis",
                newName: "Analysises");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Analysises",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "('0001-01-01T00:00:00.000')");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Analysises",
                table: "Analysises",
                column: "Id");
        }
    }
}
