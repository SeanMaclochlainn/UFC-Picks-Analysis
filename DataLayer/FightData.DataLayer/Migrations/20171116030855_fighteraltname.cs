﻿using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace FightData.DataLayer.Migrations
{
    public partial class fighteraltname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FighterId",
                table: "AltName");

            migrationBuilder.CreateTable(
                name: "FighterAltName",
                columns: table => new
                {
                    AltNameId = table.Column<int>(nullable: false),
                    FighterId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FighterAltName", x => new { x.AltNameId, x.FighterId });
                    table.ForeignKey(
                        name: "FK_FighterAltName_AltName_AltNameId",
                        column: x => x.AltNameId,
                        principalTable: "AltName",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FighterAltName_Fighter_FighterId",
                        column: x => x.FighterId,
                        principalTable: "Fighter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FighterAltName_FighterId",
                table: "FighterAltName",
                column: "FighterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FighterAltName");

            migrationBuilder.AddColumn<int>(
                name: "FighterId",
                table: "AltName",
                nullable: false,
                defaultValue: 0);
        }
    }
}