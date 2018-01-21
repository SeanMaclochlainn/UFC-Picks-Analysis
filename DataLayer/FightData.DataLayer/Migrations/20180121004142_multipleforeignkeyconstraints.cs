using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace FightData.DataLayer.Migrations
{
    public partial class multipleforeignkeyconstraints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fight_Event_EventId",
                table: "Fight");

            migrationBuilder.DropForeignKey(
                name: "FK_Pick_Analyst_AnalystId",
                table: "Pick");

            migrationBuilder.DropForeignKey(
                name: "FK_Pick_Fight_FightId",
                table: "Pick");

            migrationBuilder.DropForeignKey(
                name: "FK_Pick_Fighter_FighterPickId",
                table: "Pick");

            migrationBuilder.DropForeignKey(
                name: "FK_Webpage_Event_EventId",
                table: "Webpage");

            migrationBuilder.DropForeignKey(
                name: "FK_Webpage_Website_WebsiteId",
                table: "Webpage");

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
                name: "FighterPickId",
                table: "Pick",
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

            migrationBuilder.AlterColumn<int>(
                name: "EventId",
                table: "Fight",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Fight_Event_EventId",
                table: "Fight",
                column: "EventId",
                principalTable: "Event",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pick_Analyst_AnalystId",
                table: "Pick",
                column: "AnalystId",
                principalTable: "Analyst",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pick_Fight_FightId",
                table: "Pick",
                column: "FightId",
                principalTable: "Fight",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pick_Fighter_FighterPickId",
                table: "Pick",
                column: "FighterPickId",
                principalTable: "Fighter",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Webpage_Event_EventId",
                table: "Webpage",
                column: "EventId",
                principalTable: "Event",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Webpage_Website_WebsiteId",
                table: "Webpage",
                column: "WebsiteId",
                principalTable: "Website",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fight_Event_EventId",
                table: "Fight");

            migrationBuilder.DropForeignKey(
                name: "FK_Pick_Analyst_AnalystId",
                table: "Pick");

            migrationBuilder.DropForeignKey(
                name: "FK_Pick_Fight_FightId",
                table: "Pick");

            migrationBuilder.DropForeignKey(
                name: "FK_Pick_Fighter_FighterPickId",
                table: "Pick");

            migrationBuilder.DropForeignKey(
                name: "FK_Webpage_Event_EventId",
                table: "Webpage");

            migrationBuilder.DropForeignKey(
                name: "FK_Webpage_Website_WebsiteId",
                table: "Webpage");

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
                name: "FighterPickId",
                table: "Pick",
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

            migrationBuilder.AlterColumn<int>(
                name: "EventId",
                table: "Fight",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Fight_Event_EventId",
                table: "Fight",
                column: "EventId",
                principalTable: "Event",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_Pick_Fighter_FighterPickId",
                table: "Pick",
                column: "FighterPickId",
                principalTable: "Fighter",
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
    }
}
