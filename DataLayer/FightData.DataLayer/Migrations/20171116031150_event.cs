using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace FightData.DataLayer.Migrations
{
    public partial class @event : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "EventId",
                table: "Fight",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Fight_EventId",
                table: "Fight",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fight_Event_EventId",
                table: "Fight",
                column: "EventId",
                principalTable: "Event",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fight_Event_EventId",
                table: "Fight");

            migrationBuilder.DropIndex(
                name: "IX_Fight_EventId",
                table: "Fight");

            migrationBuilder.AlterColumn<int>(
                name: "EventId",
                table: "Fight",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
