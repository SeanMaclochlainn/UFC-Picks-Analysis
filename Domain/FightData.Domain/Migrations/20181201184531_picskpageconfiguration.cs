using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FightData.Domain.Migrations
{
    public partial class picskpageconfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PicksPageConfiguration",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PicksPageRowType = table.Column<int>(nullable: false),
                    AnalystXpath = table.Column<string>(nullable: false),
                    FighterXpath = table.Column<string>(nullable: false),
                    WebsiteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PicksPageConfiguration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PicksPageConfiguration_Website_WebsiteId",
                        column: x => x.WebsiteId,
                        principalTable: "Website",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PicksPageConfiguration_WebsiteId",
                table: "PicksPageConfiguration",
                column: "WebsiteId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PicksPageConfiguration");
        }
    }
}
