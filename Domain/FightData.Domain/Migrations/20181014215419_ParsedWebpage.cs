using Microsoft.EntityFrameworkCore.Migrations;

namespace FightData.Domain.Migrations
{
    public partial class ParsedWebpage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Parsed",
                table: "Webpage",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Parsed",
                table: "Webpage");
        }
    }
}
