﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VaccinationSystemApi.Migrations
{
    public partial class InitMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Patient",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Pesel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EMail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TimeHours",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Hour = table.Column<int>(type: "int", nullable: false),
                    Minutes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeHours", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Virus",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Virus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Certificate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Certificate_Patient_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OpeningHours",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MondayOpenId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MondayCloseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TuesdayOpenId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TuesdayCloseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    WednesdayOpenId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    WednesdayCloseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ThursdayOpenId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ThursdayCloseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FridayOpenId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FridayCloseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SaturdayOpenId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SaturdayCloseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SundayOpenId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SundayCloseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpeningHours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpeningHours_TimeHours_FridayCloseId",
                        column: x => x.FridayCloseId,
                        principalTable: "TimeHours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OpeningHours_TimeHours_FridayOpenId",
                        column: x => x.FridayOpenId,
                        principalTable: "TimeHours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OpeningHours_TimeHours_MondayCloseId",
                        column: x => x.MondayCloseId,
                        principalTable: "TimeHours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OpeningHours_TimeHours_MondayOpenId",
                        column: x => x.MondayOpenId,
                        principalTable: "TimeHours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OpeningHours_TimeHours_SaturdayCloseId",
                        column: x => x.SaturdayCloseId,
                        principalTable: "TimeHours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OpeningHours_TimeHours_SaturdayOpenId",
                        column: x => x.SaturdayOpenId,
                        principalTable: "TimeHours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OpeningHours_TimeHours_SundayCloseId",
                        column: x => x.SundayCloseId,
                        principalTable: "TimeHours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OpeningHours_TimeHours_SundayOpenId",
                        column: x => x.SundayOpenId,
                        principalTable: "TimeHours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OpeningHours_TimeHours_ThursdayCloseId",
                        column: x => x.ThursdayCloseId,
                        principalTable: "TimeHours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OpeningHours_TimeHours_ThursdayOpenId",
                        column: x => x.ThursdayOpenId,
                        principalTable: "TimeHours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OpeningHours_TimeHours_TuesdayCloseId",
                        column: x => x.TuesdayCloseId,
                        principalTable: "TimeHours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OpeningHours_TimeHours_TuesdayOpenId",
                        column: x => x.TuesdayOpenId,
                        principalTable: "TimeHours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OpeningHours_TimeHours_WednesdayCloseId",
                        column: x => x.WednesdayCloseId,
                        principalTable: "TimeHours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OpeningHours_TimeHours_WednesdayOpenId",
                        column: x => x.WednesdayOpenId,
                        principalTable: "TimeHours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VaccinationCenter",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OpeningHours_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaccinationCenter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VaccinationCenter_OpeningHours_OpeningHours_Id",
                        column: x => x.OpeningHours_Id,
                        principalTable: "OpeningHours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Doctor",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VaccinationCenter_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PatientAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Pesel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EMail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doctor_Patient_PatientAccountId",
                        column: x => x.PatientAccountId,
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Doctor_VaccinationCenter_VaccinationCenter_Id",
                        column: x => x.VaccinationCenter_Id,
                        principalTable: "VaccinationCenter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vaccine",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberOfDoses = table.Column<int>(type: "int", nullable: false),
                    MinDaysBetweenDoses = table.Column<int>(type: "int", nullable: false),
                    MaxDaysBetweenDoses = table.Column<int>(type: "int", nullable: false),
                    Virus_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MinPatientAge = table.Column<int>(type: "int", nullable: false),
                    MaxPatientAge = table.Column<int>(type: "int", nullable: false),
                    Used = table.Column<bool>(type: "bit", nullable: false),
                    VaccinationCenterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vaccine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vaccine_VaccinationCenter_VaccinationCenterId",
                        column: x => x.VaccinationCenterId,
                        principalTable: "VaccinationCenter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vaccine_Virus_Virus_Id",
                        column: x => x.Virus_Id,
                        principalTable: "Virus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TimeSlot",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    From = table.Column<DateTime>(type: "datetime2", nullable: false),
                    To = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AssignedDoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsFree = table.Column<bool>(type: "bit", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSlot", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeSlot_Doctor_AssignedDoctorId",
                        column: x => x.AssignedDoctorId,
                        principalTable: "Doctor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Appointment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WhichDose = table.Column<int>(type: "int", nullable: false),
                    TimeSlot_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Patient_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Vaccine_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    VaccineBatchNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointment_Patient_Patient_Id",
                        column: x => x.Patient_Id,
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointment_TimeSlot_TimeSlot_Id",
                        column: x => x.TimeSlot_Id,
                        principalTable: "TimeSlot",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointment_Vaccine_Vaccine_Id",
                        column: x => x.Vaccine_Id,
                        principalTable: "Vaccine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_Patient_Id",
                table: "Appointment",
                column: "Patient_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_TimeSlot_Id",
                table: "Appointment",
                column: "TimeSlot_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_Vaccine_Id",
                table: "Appointment",
                column: "Vaccine_Id");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Certificate_OwnerId",
                table: "Certificate",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_PatientAccountId",
                table: "Doctor",
                column: "PatientAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_VaccinationCenter_Id",
                table: "Doctor",
                column: "VaccinationCenter_Id");

            migrationBuilder.CreateIndex(
                name: "IX_OpeningHours_FridayCloseId",
                table: "OpeningHours",
                column: "FridayCloseId");

            migrationBuilder.CreateIndex(
                name: "IX_OpeningHours_FridayOpenId",
                table: "OpeningHours",
                column: "FridayOpenId");

            migrationBuilder.CreateIndex(
                name: "IX_OpeningHours_MondayCloseId",
                table: "OpeningHours",
                column: "MondayCloseId");

            migrationBuilder.CreateIndex(
                name: "IX_OpeningHours_MondayOpenId",
                table: "OpeningHours",
                column: "MondayOpenId");

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

            migrationBuilder.CreateIndex(
                name: "IX_TimeSlot_AssignedDoctorId",
                table: "TimeSlot",
                column: "AssignedDoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_VaccinationCenter_OpeningHours_Id",
                table: "VaccinationCenter",
                column: "OpeningHours_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Vaccine_VaccinationCenterId",
                table: "Vaccine",
                column: "VaccinationCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_Vaccine_Virus_Id",
                table: "Vaccine",
                column: "Virus_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointment");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Certificate");

            migrationBuilder.DropTable(
                name: "TimeSlot");

            migrationBuilder.DropTable(
                name: "Vaccine");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Doctor");

            migrationBuilder.DropTable(
                name: "Virus");

            migrationBuilder.DropTable(
                name: "Patient");

            migrationBuilder.DropTable(
                name: "VaccinationCenter");

            migrationBuilder.DropTable(
                name: "OpeningHours");

            migrationBuilder.DropTable(
                name: "TimeHours");
        }
    }
}
