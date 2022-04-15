using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VaccinationSystemApi.Migrations
{
    public partial class pleaseHelp2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MondayCloseId",
                table: "OpeningHours",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OpeningHours_MondayCloseId",
                table: "OpeningHours",
                column: "MondayCloseId");

            migrationBuilder.AddForeignKey(
                name: "FK_OpeningHours_TimeHours_MondayCloseId",
                table: "OpeningHours",
                column: "MondayCloseId",
                principalTable: "TimeHours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OpeningHours_TimeHours_MondayCloseId",
                table: "OpeningHours");

            migrationBuilder.DropIndex(
                name: "IX_OpeningHours_MondayCloseId",
                table: "OpeningHours");

            migrationBuilder.DropColumn(
                name: "MondayCloseId",
                table: "OpeningHours");
        }
    }
}
