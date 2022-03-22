﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VaccinationSystemApi.Models;
using VaccinationSystemApi.Models.Utils;
using VaccinationSystemApi.Repositories.Interfaces;

namespace VaccinationSystemApi.Repositories
{
    public class VaccinationSystemRepository : IVaccinationSystemRepository
    {
        private readonly List<VaccinationCenter> centers = new()
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
            return centers;
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
            var doctorFromDb = GetDoctor(doctorId);

            if (doctorFromDb is null) return null;

            return GetCenter(doctorFromDb.VaccinationCenterId);
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
                PatientId = patientId,
                TimeSlotId = timeSlotId,
                VaccineBatchNumber = "batch1",
                VaccineId = vaccineId,
                WhichDose = 1
            });
            patients.Where(x => x.Id == patientId).ToList().ForEach(s => s.Appointments.Add(createdId));
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
    }
}
