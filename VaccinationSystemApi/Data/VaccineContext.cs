using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VaccinationSystemApi.Models;

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
            modelBuilder.Entity<Appointment>().ToTable("Appointment");
            modelBuilder.Entity<Certificate>().ToTable("Certificate");
            modelBuilder.Entity<Doctor>().ToTable("Doctor");
            modelBuilder.Entity<OpeningHours>().ToTable("OpeningHours");
            modelBuilder.Entity<Patient>().ToTable("Patient");
            modelBuilder.Entity<Models.Utils.TimeHours>().ToTable("TimeHours");
            modelBuilder.Entity<TimeSlot>().ToTable("TimeSlot");
            modelBuilder.Entity<VaccinationCenter>().ToTable("VaccinationCenter");
            modelBuilder.Entity<Vaccine>().ToTable("Vaccine");
            modelBuilder.Entity<Virus>().ToTable("Virus");

            //modelBuilder.Entity<Models.Utils.TimeHours>().HasOne(th => th.OpeningHours).WithOne().OnDelete(DeleteBehavior.Restrict);
        }
    }
}
