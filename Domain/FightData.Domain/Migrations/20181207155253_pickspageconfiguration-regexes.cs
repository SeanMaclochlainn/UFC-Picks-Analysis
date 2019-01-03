using Microsoft.EntityFrameworkCore.Migrations;

namespace FightData.Domain.Migrations
{
    public partial class pickspageconfigurationregexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AnalystRegex",
                table: "PicksPageConfiguration",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FighterRegex",
                table: "PicksPageConfiguration",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnalystRegex",
                table: "PicksPageConfiguration");

            migrationBuilder.DropColumn(
                name: "FighterRegex",
                table: "PicksPageConfiguration");
        }
    }
}
