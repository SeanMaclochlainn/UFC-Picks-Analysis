using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FightData.Domain.Migrations
{
    public partial class InitialCleanCodeRefactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CardType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(unicode: false, maxLength: 180, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Exhibition",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exhibition", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fighter",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FullName = table.Column<string>(unicode: false, maxLength: 200, nullable: false),
                    FirstName = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    MiddleName = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    LastName = table.Column<string>(unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fighter", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Website",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    WebsiteName = table.Column<int>(nullable: false),
                    DomainName = table.Column<string>(unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Website", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fight",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    WinnerId = table.Column<int>(nullable: true),
                    LoserId = table.Column<int>(nullable: true),
                    ExhibitionId = table.Column<int>(nullable: false),
                    CardTypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fight", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fight_CardType_CardTypeId",
                        column: x => x.CardTypeId,
                        principalTable: "CardType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Fight_Exhibition_ExhibitionId",
                        column: x => x.ExhibitionId,
                        principalTable: "Exhibition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Fight_Fighter_LoserId",
                        column: x => x.LoserId,
                        principalTable: "Fighter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Fight_Fighter_WinnerId",
                        column: x => x.WinnerId,
                        principalTable: "Fighter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FighterAltName",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    FighterId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FighterAltName", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FighterAltName_Fighter_FighterId",
                        column: x => x.FighterId,
                        principalTable: "Fighter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Analyst",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    WebsiteId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Analyst", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Analyst_Website_WebsiteId",
                        column: x => x.WebsiteId,
                        principalTable: "Website",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Webpage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    URL = table.Column<string>(unicode: false, maxLength: 300, nullable: false),
                    WebsiteId = table.Column<int>(nullable: false),
                    ExhibitionId = table.Column<int>(nullable: false),
                    Data = table.Column<string>(unicode: false, nullable: true),
                    WebpageType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Webpage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Webpage_Exhibition_ExhibitionId",
                        column: x => x.ExhibitionId,
                        principalTable: "Exhibition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Webpage_Website_WebsiteId",
                        column: x => x.WebsiteId,
                        principalTable: "Website",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnalystAltName",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    AnalystId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalystAltName", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnalystAltName_Analyst_AnalystId",
                        column: x => x.AnalystId,
                        principalTable: "Analyst",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pick",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AnalystId = table.Column<int>(nullable: false),
                    FightId = table.Column<int>(nullable: false),
                    FighterId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pick", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pick_Analyst_AnalystId",
                        column: x => x.AnalystId,
                        principalTable: "Analyst",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pick_Fight_FightId",
                        column: x => x.FightId,
                        principalTable: "Fight",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pick_Fighter_FighterId",
                        column: x => x.FighterId,
                        principalTable: "Fighter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Analyst_WebsiteId",
                table: "Analyst",
                column: "WebsiteId");

            migrationBuilder.CreateIndex(
                name: "IX_AnalystAltName_AnalystId",
                table: "AnalystAltName",
                column: "AnalystId");

            migrationBuilder.CreateIndex(
                name: "IX_Fight_CardTypeId",
                table: "Fight",
                column: "CardTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Fight_ExhibitionId",
                table: "Fight",
                column: "ExhibitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Fight_LoserId",
                table: "Fight",
                column: "LoserId");

            migrationBuilder.CreateIndex(
                name: "IX_Fight_WinnerId",
                table: "Fight",
                column: "WinnerId");

            migrationBuilder.CreateIndex(
                name: "IX_FighterAltName_FighterId",
                table: "FighterAltName",
                column: "FighterId");

            migrationBuilder.CreateIndex(
                name: "IX_Pick_AnalystId",
                table: "Pick",
                column: "AnalystId");

            migrationBuilder.CreateIndex(
                name: "IX_Pick_FightId",
                table: "Pick",
                column: "FightId");

            migrationBuilder.CreateIndex(
                name: "IX_Pick_FighterId",
                table: "Pick",
                column: "FighterId");

            migrationBuilder.CreateIndex(
                name: "IX_Webpage_ExhibitionId",
                table: "Webpage",
                column: "ExhibitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Webpage_WebsiteId",
                table: "Webpage",
                column: "WebsiteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnalystAltName");

            migrationBuilder.DropTable(
                name: "FighterAltName");

            migrationBuilder.DropTable(
                name: "Pick");

            migrationBuilder.DropTable(
                name: "Webpage");

            migrationBuilder.DropTable(
                name: "Analyst");

            migrationBuilder.DropTable(
                name: "Fight");

            migrationBuilder.DropTable(
                name: "Website");

            migrationBuilder.DropTable(
                name: "CardType");

            migrationBuilder.DropTable(
                name: "Exhibition");

            migrationBuilder.DropTable(
                name: "Fighter");
        }
    }
}
