using Microsoft.EntityFrameworkCore.Migrations;

namespace FightData.Domain.Migrations
{
    public partial class WebsiteTypeUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WebpageType",
                table: "Webpage");

            migrationBuilder.AddColumn<int>(
                name: "WebsiteType",
                table: "Website",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WebsiteType",
                table: "Website");

            migrationBuilder.AddColumn<int>(
                name: "WebpageType",
                table: "Webpage",
                nullable: false,
                defaultValue: 0);
        }
    }
}
