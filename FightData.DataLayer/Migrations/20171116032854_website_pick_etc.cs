using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace FightData.DataLayer.Migrations
{
    public partial class website_pick_etc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "WebsiteId",
                table: "Webpage",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "EventId",
                table: "Webpage",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "FightId",
                table: "Pick",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "AnalystId",
                table: "Pick",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Webpage_EventId",
                table: "Webpage",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Webpage_WebsiteId",
                table: "Webpage",
                column: "WebsiteId");

            migrationBuilder.CreateIndex(
                name: "IX_Pick_AnalystId",
                table: "Pick",
                column: "AnalystId");

            migrationBuilder.CreateIndex(
                name: "IX_Pick_FightId",
                table: "Pick",
                column: "FightId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pick_Analyst_AnalystId",
                table: "Pick",
                column: "AnalystId",
                principalTable: "Analyst",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pick_Fight_FightId",
                table: "Pick",
                column: "FightId",
                principalTable: "Fight",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Webpage_Event_EventId",
                table: "Webpage",
                column: "EventId",
                principalTable: "Event",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Webpage_Website_WebsiteId",
                table: "Webpage",
                column: "WebsiteId",
                principalTable: "Website",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pick_Analyst_AnalystId",
                table: "Pick");

            migrationBuilder.DropForeignKey(
                name: "FK_Pick_Fight_FightId",
                table: "Pick");

            migrationBuilder.DropForeignKey(
                name: "FK_Webpage_Event_EventId",
                table: "Webpage");

            migrationBuilder.DropForeignKey(
                name: "FK_Webpage_Website_WebsiteId",
                table: "Webpage");

            migrationBuilder.DropIndex(
                name: "IX_Webpage_EventId",
                table: "Webpage");

            migrationBuilder.DropIndex(
                name: "IX_Webpage_WebsiteId",
                table: "Webpage");

            migrationBuilder.DropIndex(
                name: "IX_Pick_AnalystId",
                table: "Pick");

            migrationBuilder.DropIndex(
                name: "IX_Pick_FightId",
                table: "Pick");

            migrationBuilder.AlterColumn<int>(
                name: "WebsiteId",
                table: "Webpage",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EventId",
                table: "Webpage",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FightId",
                table: "Pick",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AnalystId",
                table: "Pick",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
