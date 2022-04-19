﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VaccinationSystemApi.Data;

namespace VaccinationSystemApi.Migrations
{
    [DbContext(typeof(VaccinationContext))]
    partial class VaccinationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.15")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("VaccinationSystemApi.Models.Appointment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("Patient_Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<Guid>("TimeslotId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("VaccineBatchNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("Vaccine_Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("WhichDose")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Patient_Id");

                    b.HasIndex("TimeslotId")
                        .IsUnique();

                    b.HasIndex("Vaccine_Id");

                    b.ToTable("Appointment");
                });

            modelBuilder.Entity("VaccinationSystemApi.Models.Certificate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Certificate");
                });

            modelBuilder.Entity("VaccinationSystemApi.Models.Doctor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("EMail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("PatientAccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Pesel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("VaccinationCenterId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PatientAccountId");

                    b.HasIndex("VaccinationCenterId");

                    b.ToTable("Doctor");
                });

            modelBuilder.Entity("VaccinationSystemApi.Models.OpeningHours", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("FridayCloseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("FridayOpenId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("MondayCloseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("MondayOpenId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("SaturdayCloseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("SaturdayOpenId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("SundayCloseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("SundayOpenId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ThursdayCloseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ThursdayOpenId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("TuesdayCloseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("TuesdayOpenId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("VaccCenterId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("WednesdayCloseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("WednesdayOpenId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("FridayCloseId");

                    b.HasIndex("FridayOpenId");

                    b.HasIndex("MondayCloseId");

                    b.HasIndex("MondayOpenId");

                    b.HasIndex("SaturdayCloseId");

                    b.HasIndex("SaturdayOpenId");

                    b.HasIndex("SundayCloseId");

                    b.HasIndex("SundayOpenId");

                    b.HasIndex("ThursdayCloseId");

                    b.HasIndex("ThursdayOpenId");

                    b.HasIndex("TuesdayCloseId");

                    b.HasIndex("TuesdayOpenId");

                    b.HasIndex("VaccCenterId")
                        .IsUnique();

                    b.HasIndex("WednesdayCloseId");

                    b.HasIndex("WednesdayOpenId");

                    b.ToTable("OpeningHours");
                });

            modelBuilder.Entity("VaccinationSystemApi.Models.Patient", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("EMail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pesel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Patient");
                });

            modelBuilder.Entity("VaccinationSystemApi.Models.TimeSlot", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<Guid>("AssignedDoctorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("From")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsFree")
                        .HasColumnType("bit");

                    b.Property<DateTime>("To")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AssignedDoctorId");

                    b.ToTable("TimeSlot");
                });

            modelBuilder.Entity("VaccinationSystemApi.Models.Utils.TimeHours", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Hour")
                        .HasColumnType("int");

                    b.Property<int>("Minutes")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("TimeHours");
                });

            modelBuilder.Entity("VaccinationSystemApi.Models.VaccinationCenter", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("VaccinationCenter");
                });

            modelBuilder.Entity("VaccinationSystemApi.Models.Vaccine", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Company")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsStillBeingUsed")
                        .HasColumnType("bit");

                    b.Property<int>("MaxDaysBetweenDoses")
                        .HasColumnType("int");

                    b.Property<int>("MaxPatientAge")
                        .HasColumnType("int");

                    b.Property<int>("MinDaysBetweenDoses")
                        .HasColumnType("int");

                    b.Property<int>("MinPatientAge")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfDoses")
                        .HasColumnType("int");

                    b.Property<Guid?>("VaccinationCenterId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("Virus_Id")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("VaccinationCenterId");

                    b.HasIndex("Virus_Id");

                    b.ToTable("Vaccine");
                });

            modelBuilder.Entity("VaccinationSystemApi.Models.Virus", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Virus");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("VaccinationSystemApi.Models.Appointment", b =>
                {
                    b.HasOne("VaccinationSystemApi.Models.Patient", "Patient_")
                        .WithMany("Appointments")
                        .HasForeignKey("Patient_Id");

                    b.HasOne("VaccinationSystemApi.Models.TimeSlot", "TimeSlot_")
                        .WithOne("AppointmentSigned")
                        .HasForeignKey("VaccinationSystemApi.Models.Appointment", "TimeslotId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VaccinationSystemApi.Models.Vaccine", "Vaccine_")
                        .WithMany()
                        .HasForeignKey("Vaccine_Id");

                    b.Navigation("Patient_");

                    b.Navigation("TimeSlot_");

                    b.Navigation("Vaccine_");
                });

            modelBuilder.Entity("VaccinationSystemApi.Models.Certificate", b =>
                {
                    b.HasOne("VaccinationSystemApi.Models.Patient", "Owner")
                        .WithMany("Certificates")
                        .HasForeignKey("OwnerId");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("VaccinationSystemApi.Models.Doctor", b =>
                {
                    b.HasOne("VaccinationSystemApi.Models.Patient", "PatientAccount")
                        .WithMany()
                        .HasForeignKey("PatientAccountId");

                    b.HasOne("VaccinationSystemApi.Models.VaccinationCenter", "VaccinationCenter_")
                        .WithMany("Doctors")
                        .HasForeignKey("VaccinationCenterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PatientAccount");

                    b.Navigation("VaccinationCenter_");
                });

            modelBuilder.Entity("VaccinationSystemApi.Models.OpeningHours", b =>
                {
                    b.HasOne("VaccinationSystemApi.Models.Utils.TimeHours", "FridayClose")
                        .WithMany()
                        .HasForeignKey("FridayCloseId");

                    b.HasOne("VaccinationSystemApi.Models.Utils.TimeHours", "FridayOpen")
                        .WithMany()
                        .HasForeignKey("FridayOpenId");

                    b.HasOne("VaccinationSystemApi.Models.Utils.TimeHours", "MondayClose")
                        .WithMany()
                        .HasForeignKey("MondayCloseId");

                    b.HasOne("VaccinationSystemApi.Models.Utils.TimeHours", "MondayOpen")
                        .WithMany()
                        .HasForeignKey("MondayOpenId");

                    b.HasOne("VaccinationSystemApi.Models.Utils.TimeHours", "SaturdayClose")
                        .WithMany()
                        .HasForeignKey("SaturdayCloseId");

                    b.HasOne("VaccinationSystemApi.Models.Utils.TimeHours", "SaturdayOpen")
                        .WithMany()
                        .HasForeignKey("SaturdayOpenId");

                    b.HasOne("VaccinationSystemApi.Models.Utils.TimeHours", "SundayClose")
                        .WithMany()
                        .HasForeignKey("SundayCloseId");

                    b.HasOne("VaccinationSystemApi.Models.Utils.TimeHours", "SundayOpen")
                        .WithMany()
                        .HasForeignKey("SundayOpenId");

                    b.HasOne("VaccinationSystemApi.Models.Utils.TimeHours", "ThursdayClose")
                        .WithMany()
                        .HasForeignKey("ThursdayCloseId");

                    b.HasOne("VaccinationSystemApi.Models.Utils.TimeHours", "ThursdayOpen")
                        .WithMany()
                        .HasForeignKey("ThursdayOpenId");

                    b.HasOne("VaccinationSystemApi.Models.Utils.TimeHours", "TuesdayClose")
                        .WithMany()
                        .HasForeignKey("TuesdayCloseId");

                    b.HasOne("VaccinationSystemApi.Models.Utils.TimeHours", "TuesdayOpen")
                        .WithMany()
                        .HasForeignKey("TuesdayOpenId");

                    b.HasOne("VaccinationSystemApi.Models.VaccinationCenter", "VaccCenter")
                        .WithOne("OpeningHours_")
                        .HasForeignKey("VaccinationSystemApi.Models.OpeningHours", "VaccCenterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VaccinationSystemApi.Models.Utils.TimeHours", "WednesdayClose")
                        .WithMany()
                        .HasForeignKey("WednesdayCloseId");

                    b.HasOne("VaccinationSystemApi.Models.Utils.TimeHours", "WednesdayOpen")
                        .WithMany()
                        .HasForeignKey("WednesdayOpenId");

                    b.Navigation("FridayClose");

                    b.Navigation("FridayOpen");

                    b.Navigation("MondayClose");

                    b.Navigation("MondayOpen");

                    b.Navigation("SaturdayClose");

                    b.Navigation("SaturdayOpen");

                    b.Navigation("SundayClose");

                    b.Navigation("SundayOpen");

                    b.Navigation("ThursdayClose");

                    b.Navigation("ThursdayOpen");

                    b.Navigation("TuesdayClose");

                    b.Navigation("TuesdayOpen");

                    b.Navigation("VaccCenter");

                    b.Navigation("WednesdayClose");

                    b.Navigation("WednesdayOpen");
                });

            modelBuilder.Entity("VaccinationSystemApi.Models.TimeSlot", b =>
                {
                    b.HasOne("VaccinationSystemApi.Models.Doctor", "AssignedDoctor")
                        .WithMany()
                        .HasForeignKey("AssignedDoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AssignedDoctor");
                });

            modelBuilder.Entity("VaccinationSystemApi.Models.Vaccine", b =>
                {
                    b.HasOne("VaccinationSystemApi.Models.VaccinationCenter", null)
                        .WithMany("AvailableVaccines")
                        .HasForeignKey("VaccinationCenterId");

                    b.HasOne("VaccinationSystemApi.Models.Virus", "Virus_")
                        .WithMany()
                        .HasForeignKey("Virus_Id");

                    b.Navigation("Virus_");
                });

            modelBuilder.Entity("VaccinationSystemApi.Models.Patient", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("Certificates");
                });

            modelBuilder.Entity("VaccinationSystemApi.Models.TimeSlot", b =>
                {
                    b.Navigation("AppointmentSigned");
                });

            modelBuilder.Entity("VaccinationSystemApi.Models.VaccinationCenter", b =>
                {
                    b.Navigation("AvailableVaccines");

                    b.Navigation("Doctors");

                    b.Navigation("OpeningHours_");
                });
#pragma warning restore 612, 618
        }
    }
}
