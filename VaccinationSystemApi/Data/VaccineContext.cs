using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using VaccinationSystemApi.Models;
using VaccinationSystemApi.Models.Utils;

namespace VaccinationSystemApi.Data
{
    public class VaccinationContext : IdentityDbContext
    {
        public VaccinationContext(DbContextOptions<VaccinationContext> options) : base(options)
        {
        }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<OpeningHours> OpeningHours { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Models.Utils.TimeHours> TimeHours { get; set; }
        public DbSet<TimeSlot> TimeSlots { get; set; }
        public DbSet<VaccinationCenter> VaccinationCenters { get; set; }
        public DbSet<Vaccine> Vaccines { get; set; }
        public DbSet<Virus> Viruses { get; set; }
        public DbSet<VaccinesToCenters> VaccinesToCenters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

/*            modelBuilder.Entity<OpeningHours>().HasOne(oh => oh.MondayOpen).WithOne().HasForeignKey<OpeningHours>(x => x.MondayOpenId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<OpeningHours>().HasOne(oh => oh.MondayClose).WithOne().HasForeignKey<OpeningHours>(x => x.MondayCloseId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<OpeningHours>().HasOne(oh => oh.TuesdayOpen).WithOne().HasForeignKey<OpeningHours>(x => x.TuesdayOpenId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<OpeningHours>().HasOne(oh => oh.TuesdayClose).WithOne().HasForeignKey<OpeningHours>(x => x.TuesdayCloseId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<OpeningHours>().HasOne(oh => oh.WednesdayOpen).WithOne().HasForeignKey<OpeningHours>(x => x.WednesdayOpenId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<OpeningHours>().HasOne(oh => oh.WednesdayClose).WithOne().HasForeignKey<OpeningHours>(x => x.WednesdayCloseId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<OpeningHours>().HasOne(oh => oh.ThursdayOpen).WithOne().HasForeignKey<OpeningHours>(x => x.ThursdayOpenId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<OpeningHours>().HasOne(oh => oh.ThursdayClose).WithOne().HasForeignKey<OpeningHours>(x => x.ThursdayCloseId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<OpeningHours>().HasOne(oh => oh.FridayOpen).WithOne().HasForeignKey<OpeningHours>(x => x.FridayOpenId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<OpeningHours>().HasOne(oh => oh.FridayClose).WithOne().HasForeignKey<OpeningHours>(x => x.FridayCloseId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<OpeningHours>().HasOne(oh => oh.SaturdayOpen).WithOne().HasForeignKey<OpeningHours>(x => x.SaturdayOpenId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<OpeningHours>().HasOne(oh => oh.SaturdayClose).WithOne().HasForeignKey<OpeningHours>(x => x.SaturdayCloseId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<OpeningHours>().HasOne(oh => oh.SundayOpen).WithOne().HasForeignKey<OpeningHours>(x => x.SundayOpenId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<OpeningHours>().HasOne(oh => oh.SundayClose).WithOne().HasForeignKey<OpeningHours>(x => x.SundayCloseId).OnDelete(DeleteBehavior.Restrict);
*/
            modelBuilder.Entity<Certificate>().ToTable("Certificate");
            modelBuilder.Entity<Doctor>().ToTable("Doctor");
            modelBuilder.Entity<OpeningHours>().ToTable("OpeningHours");
            modelBuilder.Entity<Patient>().ToTable("Patient");
            modelBuilder.Entity<Appointment>().ToTable("Appointment");
            modelBuilder.Entity<Models.Utils.TimeHours>().ToTable("TimeHours");
            modelBuilder.Entity<TimeSlot>().ToTable("TimeSlot");
            modelBuilder.Entity<VaccinationCenter>().ToTable("VaccinationCenter");
            modelBuilder.Entity<Vaccine>().ToTable("Vaccine");
            modelBuilder.Entity<Virus>().ToTable("Virus");
            modelBuilder.Entity<VaccinesToCenters>().ToTable("VaccinesToCenters");

            //SeedData(modelBuilder);
            //modelBuilder.Entity<Models.Utils.TimeHours>().HasOne(th => th.OpeningHours).WithOne().OnDelete(DeleteBehavior.Restrict);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=tcp:coredbserver14.database.windows.net,1433;Initial Catalog=coredb;Persist Security Info=False;User Id=vaccinationadmin;Password=Admin14!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;", builder =>
        //    {
        //        builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
        //    });
        //    base.OnConfiguring(optionsBuilder);
        //}

        private void SeedData(ModelBuilder modelBuilder)
        {
            var centers = new[]
            {
                new VaccinationCenter
                {
                    Active = true,
                    Id = Guid.Parse("a3c2b53f-d1f9-4e45-a4d0-0732fe1179bd"),
                    Address = "Hoża 15",
                    City = "Warszawa",
                    Name = "Centrum Szczepień na Hożej",
                }
            };

            var patients = new[]
            {
                new Patient
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Robert", //16:01 - 3h36m
                    LastName = "Lewandowski",
                    Active = true,
                    DateOfBirth = new DateTime(1989, 11, 25),
                    EMail = "rlewandowski@gmail.com",
                    Password = "",
                    Pesel = "89739200923",
                    PhoneNumber = "874888333",
                }
            };

            var viruses = new[]
            {
                new Virus
                {
                    Id = Guid.NewGuid(),
                    Name = "SARS COVID-2019"
                }
            };

            var vaccines = new[]
            {
                new Vaccine
                {
                    Id = Guid.NewGuid(),
                    Company = "Moderna Co.",
                    MaxDaysBetweenDoses = 42,
                    MaxPatientAge = 80,
                    MinDaysBetweenDoses = 21,
                    MinPatientAge = 5,
                    Name = "Moderna SuperCovidRemover",
                    NumberOfDoses = 3,
                    IsStillBeingUsed = true,
                    Virus_ = viruses[0]
                }
            };

            var appointments = new[]
            {
                new Appointment
                {
                    Id = Guid.NewGuid(),
                    Patient_ = patients[0],
                    Status = AppointmentStatus.Planned,
                    VaccineBatchNumber = "24601",
                    Vaccine_ = null,
                    TimeslotId = Guid.NewGuid(),
                    WhichDose = 1,
                }
            };

            var doctors = new[]
            {
                new Doctor
                {
                    Id = Guid.NewGuid(),
                    Active = true,
                    DateOfBirth = new DateTime(1980, 1, 1),
                    EMail = "doctorMcdoctoring@onet.pl",
                    FirstName = "Gregory",
                    LastName = "House",
                    Password = null,
                    PatientAccountId = null,
                    Pesel = "80545454545",
                    VaccinationCenterId = Guid.Parse("a3c2b53f-d1f9-4e45-a4d0-0732fe1179bd"),
                    PhoneNumber = "666666666",
                }
            };

            var timeSlots = new[]
            {
                new TimeSlot
                {
                    Id = appointments[0].TimeslotId,
                    From = new DateTime(2022, 7, 3, 8, 0, 0),
                    To = new DateTime(2022, 7, 3, 9, 0, 0),
                    AssignedDoctorId = doctors[0].Id,
                    Active = true,
                    IsFree = false,
                }
            };


            var mondayOpen1 = new TimeHours(8, 0);
            var mondayClose1 = new TimeHours(20, 0);
            var tuesdayOpen1 = new TimeHours(8, 0);
            var tuesdayClose1 = new TimeHours(20, 0);
            var wednesdayOpen1 = new TimeHours(8, 0);
            var wednesdayClose1 = new TimeHours(20, 0);
            var thursdayOpen1 = new TimeHours(8, 0);
            var thursdayClose1 = new TimeHours(20, 0);
            var fridayOpen1 = new TimeHours(8, 0);
            var fridayClose1 = new TimeHours(20, 0);
            var saturdayOpen1 = new TimeHours(8, 0);
            var saturdayClose1 = new TimeHours(20, 0);
            var sundayOpen1 = new TimeHours(8, 0);
            var sundayClose1 = new TimeHours(20, 0);
            var openingHoursId1 = Guid.NewGuid();

            var openingHours = new[]
            {
                new OpeningHours()
                {
                    Id = openingHoursId1,
                    VaccCenterId = Guid.Parse("a3c2b53f-d1f9-4e45-a4d0-0732fe1179bd"),
                    MondayOpen = mondayOpen1,
                    MondayClose = mondayClose1,
                    TuesdayOpen = tuesdayOpen1,
                    TuesdayClose = tuesdayClose1,
                    WednesdayOpen = wednesdayOpen1,
                    WednesdayClose = wednesdayClose1,
                    ThursdayOpen = thursdayOpen1,
                    ThursdayClose = thursdayClose1,
                    FridayOpen = fridayOpen1,
                    FridayClose = fridayClose1,
                    SaturdayOpen = saturdayOpen1,
                    SaturdayClose = saturdayClose1,
                    SundayOpen = sundayOpen1,
                    SundayClose = sundayClose1,
                }
            };

            modelBuilder.Entity<VaccinationCenter>().HasData(centers);
            modelBuilder.Entity<Patient>().HasData(patients);
            modelBuilder.Entity<Virus>().HasData(viruses);
            modelBuilder.Entity<Vaccine>().HasData(vaccines);
            modelBuilder.Entity<Appointment>().HasData(appointments);
            modelBuilder.Entity<Doctor>().HasData(doctors);
            modelBuilder.Entity<TimeSlot>().HasData(timeSlots);
            modelBuilder.Entity<OpeningHours>().HasData(openingHours);

        }
     }
}
