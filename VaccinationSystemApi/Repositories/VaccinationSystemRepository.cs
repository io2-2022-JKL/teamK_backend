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
        private readonly VaccinationContext _db;

        private readonly List<VaccinationCenter> centers;
        private readonly List<Patient> patients;
        private readonly List<Doctor> doctors;
        private List<TimeSlot> timeSlots;
        private List<Appointment> appointments;
        private List<Virus> viruses;
        private List<Vaccine> vaccines;

        public VaccinationSystemRepository(VaccinationContext db)
        {
            _db = db;
        }





        /*private readonly List<VaccinationCenter> centers = new()
        {
            new VaccinationCenter
            {
                Active = true,
                Address = "Street 1/45",
                AvailableVaccines = null,
                City = "Warsaw",
                Doctors = null,
                Id = Guid.Parse("a3c2b53f-d1f9-4e45-a4d0-0732fe1179bd"),
                Name = "Center1",
                OpeningHours = new TimeHours[] { new TimeHours(10, 0) },
                ClosingHours = new TimeHours[] { new TimeHours(18, 0) },
            },
            new VaccinationCenter
            {
                Active = true,
                Address = "Street2 14",
                AvailableVaccines = null,
                City = "Warsaw",
                Doctors = null,
                Id = Guid.Parse("e39f5ab2-aadf-40f5-aff8-9354cb2222d8"),
                Name = "Center2",
                OpeningHours = new TimeHours[] { new TimeHours(10, 0) },
                ClosingHours = new TimeHours[] { new TimeHours(18, 0) },
            },
            new VaccinationCenter
            {
                Active = true,
                Address = "Lwowska 15",
                AvailableVaccines = null,
                City = "Rzeszow",
                Doctors = null,
                Id = Guid.NewGuid(),
                Name = "Center3",
                OpeningHours = new TimeHours[] { new TimeHours(10, 0) },
                ClosingHours = new TimeHours[] { new TimeHours(18, 0) },
            },
        };

        private readonly List<Patient> patients = new()
        {
            new Patient
            {
                Active = true,
                Appointments = null,
                Certificates = null,
                DateOfBirth = new DateTime(2000, 11, 8),
                EMail = "patient1@mymail.com",
                FirstName = "Patient1",
                LastName = "LeSurname",
                Id = Guid.NewGuid(),
                Password = "password",
                Pesel = "001131451142",
                PhoneNumber = "661541786"
            },
            new Patient
            {
                Active = true,
                Appointments = null,
                Certificates = null,
                DateOfBirth = new DateTime(1972, 11, 8),
                EMail = "hassterplan@mymail.com",
                FirstName = "Gunther",
                LastName = "Steiner",
                Id = Guid.NewGuid(),
                Password = "doors",
                Pesel = "72110892412",
                PhoneNumber = ""
            },

        };

        private readonly List<Doctor> doctors = new()
        {
            new Doctor
            {
                Id = Guid.Parse("a3ef6dde-333c-4904-9a37-d07c72f3fa7d"),
                Active = true,
                DateOfBirth = new DateTime(1980, 01, 01),
                EMail = "mydoctor@cure.com",
                FirstName = "Doctor1",
                LastName = "Srnanme1",
                Password = "paswd",
                PatientAccountId = Guid.NewGuid(),
                Pesel = "80010141181",
                PhoneNumber = "666444333",
                VaccinationCenterId = Guid.Parse("a3c2b53f-d1f9-4e45-a4d0-0732fe1179bd"),   
            },    
            new Doctor
            {
                Id = Guid.Parse("9efdcda1-0b9a-4cab-82e4-08382cda3851"),
                Active = true,
                DateOfBirth = new DateTime(1980, 01, 01),
                EMail = "mydoctor@cure.com",
                FirstName = "Doctor2",
                LastName = "Srnanme2",
                Password = "paswd",
                PatientAccountId = Guid.NewGuid(),
                Pesel = "80010141181",
                PhoneNumber = "666444333",
                VaccinationCenterId = Guid.Parse("e39f5ab2-aadf-40f5-aff8-9354cb2222d8"),   
            }
        };

        private List<TimeSlot> timeSlots = new()
        {
            new TimeSlot
            {
                Id = Guid.NewGuid(),
                Active = true,
                IsFree = true,
                From = new DateTime(2022, 05, 10, 12, 00, 00),
                To = new DateTime(2022, 05, 10, 13, 00, 00),
                DoctorId = Guid.Parse("9efdcda1-0b9a-4cab-82e4-08382cda3851"),
            },
            new TimeSlot
            {
                Id = Guid.NewGuid(),
                Active = true,
                IsFree = true,
                From = new DateTime(2022, 05, 11, 15, 00, 00),
                To = new DateTime(2022, 05, 11, 15, 30, 00),
                DoctorId = Guid.Parse("9efdcda1-0b9a-4cab-82e4-08382cda3851"),
            },
            new TimeSlot
            {
                Id = Guid.NewGuid(),
                Active = true,
                IsFree = true,
                From = new DateTime(2022, 05, 10, 13, 00, 00),
                To = new DateTime(2022, 05, 10, 14, 00, 00),
                DoctorId = Guid.Parse("9efdcda1-0b9a-4cab-82e4-08382cda3851"),
            },
            new TimeSlot
            {
                Id = Guid.NewGuid(),
                Active = true,
                IsFree = true,
                From = new DateTime(2022, 05, 10, 13, 00, 00),
                To = new DateTime(2022, 05, 10, 14, 00, 00),
                DoctorId = Guid.Parse("a3ef6dde-333c-4904-9a37-d07c72f3fa7d"),
            },


        };

        private List<Appointment> appointments = new();

        var v1 = new Virus()
        {
            Id = Guid.NewGuid(),
            Name = "Coronavirus"
        };

        private List<Virus> viruses = new List<Virus>()
        {
            
        };

        private List<Vaccine> vaccines = new List<Vaccine>() {
            new Vaccine() {
            Id = Guid.NewGuid(),
            Company = "Pfizer",
            MaxDaysBetweenDoses = 28,
            MaxPatientAge = 73,
            MinDaysBetweenDoses = 21,
            MinPatientAge = 15,
            Name = "Pfizer Vaccine Google Chrome",
            NumberOfDoses = 3,
            Used = true,
            Virus_ = viruses[0]
            }
        };*/


        public Patient GetPatient(Guid id)
        {
            return patients.Where(x => x.Id == id).FirstOrDefault();
        }

        public IEnumerable<Patient> GetPatients()
        {
            return patients;
        }

        public IEnumerable<VaccinationCenter> GetCenters()
        {
            return _db.VaccinationCenters;
        }

        public VaccinationCenter GetCenter(Guid id)
        {
            return centers.Where(x => x.Id == id).SingleOrDefault();
        }

        public IEnumerable<Doctor> GetDoctors()
        {
            return doctors;
        }

        public Doctor GetDoctor(Guid id)
        {
            return doctors.Where(x => x.Id == id).SingleOrDefault();
        }

        public VaccinationCenter GetCenterOfDoctor(Guid doctorId)
        {
            var doctorFromDb = doctors.Where(x => x.Id == doctorId).SingleOrDefault();

            if (doctorFromDb is null) return null;

            return doctorFromDb.VaccinationCenter_;
        }
        public IEnumerable<TimeSlot> GetTimeSlots()
        {
            return timeSlots;
        }

        public Guid CreateAppointment(Guid patientId, Guid timeSlotId, Guid vaccineId)
        {
            Guid createdId = Guid.NewGuid();
            appointments.Add(new Appointment
            {
                Id = createdId,
                Status = AppointmentStatus.Planned,
                Patient_ = patients.Where(x => x.Id == patientId).SingleOrDefault(),
                TimeSlot_ = timeSlots.Where(t => t.Id == timeSlotId).SingleOrDefault(),
                VaccineBatchNumber = "batch1",
                Vaccine_ = new Vaccine(),
                WhichDose = 1
            });
            
            return createdId;
        }

        public Appointment GetAppointment(Guid id)
        {
            return appointments.Where(x => x.Id == id).SingleOrDefault();
        }

        public void CancelAppointment(Guid id)
        {
            appointments.Where(x => x.Id == id).ToList().ForEach(s => s.Status = AppointmentStatus.Cancelled);
        }

        public void CreateTimeSlot(TimeSlot timeSlot)
        {
            timeSlots.Add(timeSlot);
        }

        public void DeleteTimeSlot(Guid id)
        {
            timeSlots.Where(x => x.Id == id).ToList().ForEach(s => s.IsFree = false);
        }
        public IEnumerable<TimeSlot> GetDoctorActiveSlots(Guid doctorId, string date)
        {
            return _db.TimeSlots.Where(x => x.AssignedDoctorId == doctorId && x.From.ToShortDateString() == date
                && x.Active == true);
        }

        public Doctor GetDoctorByTimeSlot(Guid id)
        {
            var slotFromDb = timeSlots.Where(x => x.Id == id).SingleOrDefault();
            if (slotFromDb is null) return null;
            return this.GetDoctor(slotFromDb.AssignedDoctorId);
        }
        public void ModifyTimeSlot(Guid timeSlotId, DateTime from, DateTime to)
        {
            timeSlots.Where(x => x.Id == timeSlotId).ToList().ForEach(s => { s.From = from; s.To = to; });
        }

        public IEnumerable<Appointment> GetIncomingAppointments(Guid patientId)
        {
            return _db.Appointments.Where(a => a.Patient_.Id == patientId && a.TimeSlot_.From > DateTime.Now)
                .Include(a => a.TimeSlot_).ThenInclude(t => t.AssignedDoctor).ThenInclude(d => d.VaccinationCenter_)
                .Include(a => a.Vaccine_);
        }

        public IEnumerable<Appointment> GetFormerAppointments(Guid patientId)
        {
            return _db.Appointments.Where(a => a.Patient_.Id == patientId && a.TimeSlot_.From < DateTime.Now)
                .Include(a => a.TimeSlot_).ThenInclude(t => t.AssignedDoctor).ThenInclude(d => d.VaccinationCenter_)
                .Include(a => a.Vaccine_);
        }

        public IEnumerable<TimeSlot> FilterTimeslots(string city, DateTime dateFrom, DateTime dateTo, string virus)
        {
            return _db.TimeSlots.Include(t => t.AssignedDoctor)
                .ThenInclude(d => d.VaccinationCenter_)
                .ThenInclude(vc => vc.AvailableVaccines)
                .ThenInclude(v => v.Virus_)
                .Where(t => t.AssignedDoctor.VaccinationCenter_.City == city && t.From == dateFrom 
                && t.To == dateTo && t.AssignedDoctor.VaccinationCenter_.AvailableVaccines.Select(v => v.Virus_.Name == virus).Count() > 0);
            // timeSlot isn't directly related to virus
        }


    }
}
