using Microsoft.EntityFrameworkCore.Migrations;

namespace VaccinationSystemApi.Migrations
{
    public partial class DoctorId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DoctorId",
                table: "TimeSlot",
                newName: "AssignedDoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSlot_AssignedDoctorId",
                table: "TimeSlot",
                column: "AssignedDoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSlot_Doctor_AssignedDoctorId",
                table: "TimeSlot",
                column: "AssignedDoctorId",
                principalTable: "Doctor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeSlot_Doctor_AssignedDoctorId",
                table: "TimeSlot");

            migrationBuilder.DropIndex(
                name: "IX_TimeSlot_AssignedDoctorId",
                table: "TimeSlot");

            migrationBuilder.RenameColumn(
                name: "AssignedDoctorId",
                table: "TimeSlot",
                newName: "DoctorId");
        }
    }
}
