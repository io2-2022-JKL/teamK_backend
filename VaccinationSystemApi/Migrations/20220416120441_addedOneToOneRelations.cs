using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VaccinationSystemApi.Migrations
{
    public partial class addedOneToOneRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FridayCloseId",
                table: "OpeningHours",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FridayOpenId",
                table: "OpeningHours",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SaturdayCloseId",
                table: "OpeningHours",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SaturdayOpenId",
                table: "OpeningHours",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SundayCloseId",
                table: "OpeningHours",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SundayOpenId",
                table: "OpeningHours",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ThursdayCloseId",
                table: "OpeningHours",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ThursdayOpenId",
                table: "OpeningHours",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TuesdayCloseId",
                table: "OpeningHours",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TuesdayOpenId",
                table: "OpeningHours",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WednesdayCloseId",
                table: "OpeningHours",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WednesdayOpenId",
                table: "OpeningHours",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OpeningHours_FridayCloseId",
                table: "OpeningHours",
                column: "FridayCloseId");

            migrationBuilder.CreateIndex(
                name: "IX_OpeningHours_FridayOpenId",
                table: "OpeningHours",
                column: "FridayOpenId");

            migrationBuilder.CreateIndex(
                name: "IX_OpeningHours_SaturdayCloseId",
                table: "OpeningHours",
                column: "SaturdayCloseId");

            migrationBuilder.CreateIndex(
                name: "IX_OpeningHours_SaturdayOpenId",
                table: "OpeningHours",
                column: "SaturdayOpenId");

            migrationBuilder.CreateIndex(
                name: "IX_OpeningHours_SundayCloseId",
                table: "OpeningHours",
                column: "SundayCloseId");

            migrationBuilder.CreateIndex(
                name: "IX_OpeningHours_SundayOpenId",
                table: "OpeningHours",
                column: "SundayOpenId");

            migrationBuilder.CreateIndex(
                name: "IX_OpeningHours_ThursdayCloseId",
                table: "OpeningHours",
                column: "ThursdayCloseId");

            migrationBuilder.CreateIndex(
                name: "IX_OpeningHours_ThursdayOpenId",
                table: "OpeningHours",
                column: "ThursdayOpenId");

            migrationBuilder.CreateIndex(
                name: "IX_OpeningHours_TuesdayCloseId",
                table: "OpeningHours",
                column: "TuesdayCloseId");

            migrationBuilder.CreateIndex(
                name: "IX_OpeningHours_TuesdayOpenId",
                table: "OpeningHours",
                column: "TuesdayOpenId");

            migrationBuilder.CreateIndex(
                name: "IX_OpeningHours_WednesdayCloseId",
                table: "OpeningHours",
                column: "WednesdayCloseId");

            migrationBuilder.CreateIndex(
                name: "IX_OpeningHours_WednesdayOpenId",
                table: "OpeningHours",
                column: "WednesdayOpenId");

            migrationBuilder.AddForeignKey(
                name: "FK_OpeningHours_TimeHours_FridayCloseId",
                table: "OpeningHours",
                column: "FridayCloseId",
                principalTable: "TimeHours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OpeningHours_TimeHours_FridayOpenId",
                table: "OpeningHours",
                column: "FridayOpenId",
                principalTable: "TimeHours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OpeningHours_TimeHours_SaturdayCloseId",
                table: "OpeningHours",
                column: "SaturdayCloseId",
                principalTable: "TimeHours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OpeningHours_TimeHours_SaturdayOpenId",
                table: "OpeningHours",
                column: "SaturdayOpenId",
                principalTable: "TimeHours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OpeningHours_TimeHours_SundayCloseId",
                table: "OpeningHours",
                column: "SundayCloseId",
                principalTable: "TimeHours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OpeningHours_TimeHours_SundayOpenId",
                table: "OpeningHours",
                column: "SundayOpenId",
                principalTable: "TimeHours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OpeningHours_TimeHours_ThursdayCloseId",
                table: "OpeningHours",
                column: "ThursdayCloseId",
                principalTable: "TimeHours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OpeningHours_TimeHours_ThursdayOpenId",
                table: "OpeningHours",
                column: "ThursdayOpenId",
                principalTable: "TimeHours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OpeningHours_TimeHours_TuesdayCloseId",
                table: "OpeningHours",
                column: "TuesdayCloseId",
                principalTable: "TimeHours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OpeningHours_TimeHours_TuesdayOpenId",
                table: "OpeningHours",
                column: "TuesdayOpenId",
                principalTable: "TimeHours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OpeningHours_TimeHours_WednesdayCloseId",
                table: "OpeningHours",
                column: "WednesdayCloseId",
                principalTable: "TimeHours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OpeningHours_TimeHours_WednesdayOpenId",
                table: "OpeningHours",
                column: "WednesdayOpenId",
                principalTable: "TimeHours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OpeningHours_TimeHours_FridayCloseId",
                table: "OpeningHours");

            migrationBuilder.DropForeignKey(
                name: "FK_OpeningHours_TimeHours_FridayOpenId",
                table: "OpeningHours");

            migrationBuilder.DropForeignKey(
                name: "FK_OpeningHours_TimeHours_SaturdayCloseId",
                table: "OpeningHours");

            migrationBuilder.DropForeignKey(
                name: "FK_OpeningHours_TimeHours_SaturdayOpenId",
                table: "OpeningHours");

            migrationBuilder.DropForeignKey(
                name: "FK_OpeningHours_TimeHours_SundayCloseId",
                table: "OpeningHours");

            migrationBuilder.DropForeignKey(
                name: "FK_OpeningHours_TimeHours_SundayOpenId",
                table: "OpeningHours");

            migrationBuilder.DropForeignKey(
                name: "FK_OpeningHours_TimeHours_ThursdayCloseId",
                table: "OpeningHours");

            migrationBuilder.DropForeignKey(
                name: "FK_OpeningHours_TimeHours_ThursdayOpenId",
                table: "OpeningHours");

            migrationBuilder.DropForeignKey(
                name: "FK_OpeningHours_TimeHours_TuesdayCloseId",
                table: "OpeningHours");

            migrationBuilder.DropForeignKey(
                name: "FK_OpeningHours_TimeHours_TuesdayOpenId",
                table: "OpeningHours");

            migrationBuilder.DropForeignKey(
                name: "FK_OpeningHours_TimeHours_WednesdayCloseId",
                table: "OpeningHours");

            migrationBuilder.DropForeignKey(
                name: "FK_OpeningHours_TimeHours_WednesdayOpenId",
                table: "OpeningHours");

            migrationBuilder.DropIndex(
                name: "IX_OpeningHours_FridayCloseId",
                table: "OpeningHours");

            migrationBuilder.DropIndex(
                name: "IX_OpeningHours_FridayOpenId",
                table: "OpeningHours");

            migrationBuilder.DropIndex(
                name: "IX_OpeningHours_SaturdayCloseId",
                table: "OpeningHours");

            migrationBuilder.DropIndex(
                name: "IX_OpeningHours_SaturdayOpenId",
                table: "OpeningHours");

            migrationBuilder.DropIndex(
                name: "IX_OpeningHours_SundayCloseId",
                table: "OpeningHours");

            migrationBuilder.DropIndex(
                name: "IX_OpeningHours_SundayOpenId",
                table: "OpeningHours");

            migrationBuilder.DropIndex(
                name: "IX_OpeningHours_ThursdayCloseId",
                table: "OpeningHours");

            migrationBuilder.DropIndex(
                name: "IX_OpeningHours_ThursdayOpenId",
                table: "OpeningHours");

            migrationBuilder.DropIndex(
                name: "IX_OpeningHours_TuesdayCloseId",
                table: "OpeningHours");

            migrationBuilder.DropIndex(
                name: "IX_OpeningHours_TuesdayOpenId",
                table: "OpeningHours");

            migrationBuilder.DropIndex(
                name: "IX_OpeningHours_WednesdayCloseId",
                table: "OpeningHours");

            migrationBuilder.DropIndex(
                name: "IX_OpeningHours_WednesdayOpenId",
                table: "OpeningHours");

            migrationBuilder.DropColumn(
                name: "FridayCloseId",
                table: "OpeningHours");

            migrationBuilder.DropColumn(
                name: "FridayOpenId",
                table: "OpeningHours");

            migrationBuilder.DropColumn(
                name: "SaturdayCloseId",
                table: "OpeningHours");

            migrationBuilder.DropColumn(
                name: "SaturdayOpenId",
                table: "OpeningHours");

            migrationBuilder.DropColumn(
                name: "SundayCloseId",
                table: "OpeningHours");

            migrationBuilder.DropColumn(
                name: "SundayOpenId",
                table: "OpeningHours");

            migrationBuilder.DropColumn(
                name: "ThursdayCloseId",
                table: "OpeningHours");

            migrationBuilder.DropColumn(
                name: "ThursdayOpenId",
                table: "OpeningHours");

            migrationBuilder.DropColumn(
                name: "TuesdayCloseId",
                table: "OpeningHours");

            migrationBuilder.DropColumn(
                name: "TuesdayOpenId",
                table: "OpeningHours");

            migrationBuilder.DropColumn(
                name: "WednesdayCloseId",
                table: "OpeningHours");

            migrationBuilder.DropColumn(
                name: "WednesdayOpenId",
                table: "OpeningHours");
        }
    }
}
