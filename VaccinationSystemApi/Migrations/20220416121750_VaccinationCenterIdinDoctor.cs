using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VaccinationSystemApi.Migrations
{
    public partial class VaccinationCenterIdinDoctor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctor_VaccinationCenter_VaccinationCenter_Id",
                table: "Doctor");

            migrationBuilder.DropIndex(
                name: "IX_Doctor_VaccinationCenter_Id",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "VaccinationCenter_Id",
                table: "Doctor");

            migrationBuilder.AddColumn<Guid>(
                name: "VaccinationCenterId",
                table: "Doctor",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_VaccinationCenterId",
                table: "Doctor",
                column: "VaccinationCenterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctor_VaccinationCenter_VaccinationCenterId",
                table: "Doctor",
                column: "VaccinationCenterId",
                principalTable: "VaccinationCenter",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctor_VaccinationCenter_VaccinationCenterId",
                table: "Doctor");

            migrationBuilder.DropIndex(
                name: "IX_Doctor_VaccinationCenterId",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "VaccinationCenterId",
                table: "Doctor");

            migrationBuilder.AddColumn<Guid>(
                name: "VaccinationCenter_Id",
                table: "Doctor",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_VaccinationCenter_Id",
                table: "Doctor",
                column: "VaccinationCenter_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctor_VaccinationCenter_VaccinationCenter_Id",
                table: "Doctor",
                column: "VaccinationCenter_Id",
                principalTable: "VaccinationCenter",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
