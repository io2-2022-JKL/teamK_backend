using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VaccinationSystemApi.Models;
using VaccinationSystemApi.Models.Utils;
using VaccinationSystemApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using VaccinationSystemApi.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VaccinationSystemApi.Repositories
{
    public class VaccinationSystemRepository : IVaccinationSystemRepository
    {
        private readonly VaccinationContext _dbContext;

        private readonly List<VaccinationCenter> centers;
        private readonly List<Patient> patients;
        private readonly List<Doctor> doctors;
        private List<TimeSlot> timeSlots;
        private List<Appointment> appointments;
        private List<Virus> viruses;
        private List<Vaccine> vaccines;

        public VaccinationSystemRepository(VaccinationContext db)
        {
            _dbContext = db;
        }


       


        public Patient GetPatient(Guid id)
        {
            return _dbContext.Patients.Where(x => x.Id == id).FirstOrDefault();
        }

        public IEnumerable<Patient> GetPatients()
        {
            return _dbContext.Patients;
        }

        public IEnumerable<VaccinationCenter> GetCenters()
        {
            return _dbContext.VaccinationCenters;
        }

        public VaccinationCenter GetCenter(Guid id)
        {
            return _dbContext.VaccinationCenters.Where(x => x.Id == id).SingleOrDefault();
        }

        public IEnumerable<Doctor> GetDoctors()
        {
            return _dbContext.Doctors;
        }

        public Doctor GetDoctor(Guid id)
        {
            return _dbContext.Doctors.Where(x => x.Id == id).SingleOrDefault();
        }

        public VaccinationCenter GetCenterOfDoctor(Guid doctorId)
        {
            var doctorFromDb = _dbContext.Doctors.Where(x => x.Id == doctorId).SingleOrDefault();

            if (doctorFromDb is null) return null;

            return doctorFromDb.VaccinationCenter_;
        }
        public IEnumerable<TimeSlot> GetTimeSlots()
        {
            return _dbContext.TimeSlots;
        }

        public Guid CreateAppointment(Guid patientId, Guid timeSlotId, Guid vaccineId)
        {
            Guid createdId = Guid.NewGuid();
            _dbContext.Appointments.Add(new Appointment
            {
                Id = createdId,
                Status = AppointmentStatus.Planned,
                Patient_ = patients.Where(x => x.Id == patientId).SingleOrDefault(),
                TimeSlot_ = timeSlots.Where(t => t.Id == timeSlotId).SingleOrDefault(),
                VaccineBatchNumber = "batch1",
                Vaccine_ = new Vaccine(),
                WhichDose = 1
            });

            _dbContext.SaveChanges();
            return createdId;
        }

        public Appointment GetAppointment(Guid id)
        {
            return _dbContext.Appointments.Where(x => x.Id == id).SingleOrDefault();
        }

        public void CancelAppointment(Guid id)
        {
            var appointmentFromDb = _dbContext.Appointments.Where(x => x.Id == id).SingleOrDefault();
            if (appointmentFromDb == null) return;

            appointmentFromDb.Status = AppointmentStatus.Cancelled;
            _dbContext.SaveChanges();
        }

        public void CreateTimeSlot(TimeSlot timeSlot)
        {
            _dbContext.TimeSlots.Add(timeSlot);
            _dbContext.SaveChanges();
        }

        public void DeleteTimeSlot(Guid id)
        {
            var slotFromDb = _dbContext.TimeSlots.Where(x => x.Id == id).SingleOrDefault();
            if (slotFromDb == null) return;

            slotFromDb.IsFree = false;
            _dbContext.SaveChanges();
        }
        public IEnumerable<TimeSlot> GetDoctorActiveSlots(Guid doctorId, string date)
        {
            return _dbContext.TimeSlots.Where(x => x.AssignedDoctorId == doctorId && x.From.ToShortDateString() == date
                && x.Active == true);
        }

        public Doctor GetDoctorByTimeSlot(Guid id)
        {
            var slotFromDb = _dbContext.TimeSlots.Where(x => x.Id == id).SingleOrDefault();
            if (slotFromDb is null) return null;
            return GetDoctor(slotFromDb.AssignedDoctorId);
        }
        public void ModifyTimeSlot(Guid timeSlotId, DateTime from, DateTime to)
        {
            _dbContext.TimeSlots.Where(x => x.Id == timeSlotId).ToList().ForEach(s => { s.From = from; s.To = to; });
        }

        public IEnumerable<Appointment> GetIncomingAppointments(Guid patientId)
        {
            return _dbContext.Appointments.Where(a => a.Patient_.Id == patientId && a.TimeSlot_.From > DateTime.Now)
                .Include(a => a.TimeSlot_).ThenInclude(t => t.AssignedDoctor).ThenInclude(d => d.VaccinationCenter_)
                .Include(a => a.Vaccine_);
        }

        public IEnumerable<Appointment> GetFormerAppointments(Guid patientId)
        {
            return _dbContext.Appointments.Where(a => a.Patient_.Id == patientId && a.TimeSlot_.From < DateTime.Now)
                .Include(a => a.TimeSlot_).ThenInclude(t => t.AssignedDoctor).ThenInclude(d => d.VaccinationCenter_)
                .Include(a => a.Vaccine_);
        }

        public IEnumerable<TimeSlot> FilterTimeslots(string city, DateTime dateFrom, DateTime dateTo, string virus)
        {
            return _dbContext.TimeSlots.Include(t => t.AssignedDoctor)
                .ThenInclude(d => d.VaccinationCenter_)
                .ThenInclude(vc => vc.AvailableVaccines)
                .ThenInclude(v => v.Virus_)
                .Where(t => t.AssignedDoctor.VaccinationCenter_.City == city && t.From == dateFrom 
                && t.To == dateTo && t.AssignedDoctor.VaccinationCenter_.AvailableVaccines.Select(v => v.Virus_.Name == virus).Count() > 0);
            // timeSlot isn't directly related to virus
        }
    }
}
