using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace FightData.DataLayer.Migrations
{
    public partial class PickTableUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Correct",
                table: "Pick");

            migrationBuilder.DropColumn(
                name: "Pick",
                table: "Pick");

            migrationBuilder.AlterColumn<int>(
                name: "FighterPickId",
                table: "Pick",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Pick_FighterPickId",
                table: "Pick",
                column: "FighterPickId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pick_Fighter_FighterPickId",
                table: "Pick",
                column: "FighterPickId",
                principalTable: "Fighter",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pick_Fighter_FighterPickId",
                table: "Pick");

            migrationBuilder.DropIndex(
                name: "IX_Pick_FighterPickId",
                table: "Pick");

            migrationBuilder.AlterColumn<int>(
                name: "FighterPickId",
                table: "Pick",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Correct",
                table: "Pick",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Pick",
                table: "Pick",
                unicode: false,
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
