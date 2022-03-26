using Microsoft.EntityFrameworkCore;
using VaccinationSystemApi.Models;

namespace VaccinationSystemApi.Data
{
    public class VaccinationContext : DbContext
    {
        public VaccinationContext(DbContextOptions<VaccinationContext> options) : base(options)
        {
        }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<TimeSlot> TimeSlots { get; set; }
        public DbSet<VaccinationCenter> VaccinationCenters { get; set; }
        public DbSet<Vaccine> Vaccines { get; set; }
        public DbSet<Virus> Viruses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>().ToTable("Appointment");
            modelBuilder.Entity<Certificate>().ToTable("Certificate");
            modelBuilder.Entity<Doctor>().ToTable("Doctor");
            modelBuilder.Entity<Patient>().ToTable("Patient");
            modelBuilder.Entity<TimeSlot>().ToTable("TimeSlot");
            modelBuilder.Entity<VaccinationCenter>().ToTable("VaccinationCenter");
            modelBuilder.Entity<Vaccine>().ToTable("Vaccine");
            modelBuilder.Entity<Virus>().ToTable("Virus");
        }
    }
}
