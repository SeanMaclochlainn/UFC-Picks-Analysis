using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace FightData.DataLayer.Migrations
{
    public partial class cardtype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CardTypeId",
                table: "Fight",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Fight_CardTypeId",
                table: "Fight",
                column: "CardTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fight_CardType_CardTypeId",
                table: "Fight",
                column: "CardTypeId",
                principalTable: "CardType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fight_CardType_CardTypeId",
                table: "Fight");

            migrationBuilder.DropIndex(
                name: "IX_Fight_CardTypeId",
                table: "Fight");

            migrationBuilder.AlterColumn<int>(
                name: "CardTypeId",
                table: "Fight",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
