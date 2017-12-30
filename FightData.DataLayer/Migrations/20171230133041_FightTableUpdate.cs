using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace FightData.DataLayer.Migrations
{
    public partial class FightTableUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FighterAId",
                table: "Fight");

            migrationBuilder.DropColumn(
                name: "FighterBId",
                table: "Fight");

            migrationBuilder.AlterColumn<int>(
                name: "WinnerId",
                table: "Fight",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "LoserId",
                table: "Fight",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Fight_LoserId",
                table: "Fight",
                column: "LoserId");

            migrationBuilder.CreateIndex(
                name: "IX_Fight_WinnerId",
                table: "Fight",
                column: "WinnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fight_Fighter_LoserId",
                table: "Fight",
                column: "LoserId",
                principalTable: "Fighter",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Fight_Fighter_WinnerId",
                table: "Fight",
                column: "WinnerId",
                principalTable: "Fighter",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fight_Fighter_LoserId",
                table: "Fight");

            migrationBuilder.DropForeignKey(
                name: "FK_Fight_Fighter_WinnerId",
                table: "Fight");

            migrationBuilder.DropIndex(
                name: "IX_Fight_LoserId",
                table: "Fight");

            migrationBuilder.DropIndex(
                name: "IX_Fight_WinnerId",
                table: "Fight");

            migrationBuilder.DropColumn(
                name: "LoserId",
                table: "Fight");

            migrationBuilder.AlterColumn<int>(
                name: "WinnerId",
                table: "Fight",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FighterAId",
                table: "Fight",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FighterBId",
                table: "Fight",
                nullable: false,
                defaultValue: 0);
        }
    }
}
