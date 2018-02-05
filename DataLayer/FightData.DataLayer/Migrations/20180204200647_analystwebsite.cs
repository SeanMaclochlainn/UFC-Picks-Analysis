using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace FightData.DataLayer.Migrations
{
    public partial class analystwebsite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WebsiteId",
                table: "Analyst",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Analyst_WebsiteId",
                table: "Analyst",
                column: "WebsiteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Analyst_Website_WebsiteId",
                table: "Analyst",
                column: "WebsiteId",
                principalTable: "Website",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Analyst_Website_WebsiteId",
                table: "Analyst");

            migrationBuilder.DropIndex(
                name: "IX_Analyst_WebsiteId",
                table: "Analyst");

            migrationBuilder.DropColumn(
                name: "WebsiteId",
                table: "Analyst");
        }
    }
}
