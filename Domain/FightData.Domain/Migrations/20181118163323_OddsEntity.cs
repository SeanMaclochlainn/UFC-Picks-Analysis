using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FightData.Domain.Migrations
{
    public partial class OddsEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Odd",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<decimal>(nullable: false),
                    FighterId = table.Column<int>(nullable: false),
                    FightId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Odd", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Odd_Fight_FightId",
                        column: x => x.FightId,
                        principalTable: "Fight",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Odd_Fighter_FighterId",
                        column: x => x.FighterId,
                        principalTable: "Fighter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Odd_FightId",
                table: "Odd",
                column: "FightId");

            migrationBuilder.CreateIndex(
                name: "IX_Odd_FighterId",
                table: "Odd",
                column: "FighterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Odd");
        }
    }
}
