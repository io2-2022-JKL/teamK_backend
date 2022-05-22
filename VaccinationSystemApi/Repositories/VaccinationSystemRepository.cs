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
using VaccinationSystemApi.Dtos.Login;
using VaccinationSystemApi.Dtos.Admin;
using VaccinationSystemApi.Exceptions;
using VaccinationSystemApi.Services;

namespace VaccinationSystemApi.Repositories
{
    public class VaccinationSystemRepository : IVaccinationSystemRepository
    {
        private readonly VaccinationContext _dbContext;
        private readonly TimeHoursService _timeHoursService;

        public VaccinationSystemRepository(VaccinationContext db)
        {
            _dbContext = db;
            _timeHoursService = new TimeHoursService();
        }
        public Patient GetPatient(Guid id)
        {
            return _dbContext.Patients.Where(x => x.Id == id).FirstOrDefault();
        }

        public IEnumerable<Patient> GetPatients()
        {
            return _dbContext.Patients;
        }

        public IEnumerable<Certificate> GetCertificates()
        {
            return _dbContext.Certificates;
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
            var doctorFromDb = _dbContext.Doctors.Where(x => x.Id == doctorId).Include(d => d.VaccinationCenter_).SingleOrDefault();

            if (doctorFromDb is null) return null;

            return doctorFromDb.VaccinationCenter_;
        }

        public TimeSlot GetTimeSlot(Guid timeSlotId)
        {
            return _dbContext.TimeSlots.Where(t => t.Id == timeSlotId).SingleOrDefault();
        }
        public IEnumerable<TimeSlot> GetTimeSlots()
        {
            return _dbContext.TimeSlots.ToList();
        }

        public IEnumerable<Appointment> GetAppointments()
        {
            return _dbContext.Appointments.Include(a => a.Patient_).Include(a => a.Vaccine_).ThenInclude(v => v.Virus_).ToList();
        }

        public IEnumerable<Certificate> GetPatientCertificates(Guid patientId)
        {
            var patientFromDb = this.GetPatient(patientId);

            return patientFromDb.Certificates;
        }

        public Guid CreateAppointment(Guid patientId, Guid timeSlotId, Guid vaccineId)
        {
            Guid createdId = Guid.NewGuid();
            _dbContext.Appointments.Add(new Appointment
            {
                Id = createdId,
                Status = AppointmentStatus.Planned,
                Patient_ = _dbContext.Patients.Where(x => x.Id == patientId).SingleOrDefault(),
                TimeSlot_ = _dbContext.TimeSlots.Where(t => t.Id == timeSlotId).SingleOrDefault(),
                VaccineBatchNumber = "batch1",
                Vaccine_ = _dbContext.Vaccines.Where(v => v.Id == vaccineId).SingleOrDefault(),
                WhichDose = 1
            });

            _dbContext.SaveChanges();
            return createdId;
        }

        public Appointment GetAppointment(Guid id)
        {
            return _dbContext.Appointments.Where(x => x.Id == id).Include(a => a.Patient_)
                .Include(a => a.TimeSlot_).ThenInclude(t => t.AssignedDoctor)
                .Include(a => a.Vaccine_).ThenInclude(v => v.Virus_).SingleOrDefault();
        }

        public void CancelAppointment(Guid id)
        {
            var appointmentFromDb = _dbContext.Appointments.Where(x => x.Id == id).SingleOrDefault();
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

        public IEnumerable<TimeSlot> GetDoctorTimeSlots(Guid doctorId)
        {
            return _dbContext.TimeSlots.Where(x => x.AssignedDoctorId == doctorId);
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
            _dbContext.SaveChanges();
        }

        public IEnumerable<Appointment> GetIncomingAppointments(Guid patientId)
        {
            return _dbContext.Appointments.Where(a => a.Patient_.Id == patientId && a.TimeSlot_.From > DateTime.Now)
                .Include(a => a.TimeSlot_).ThenInclude(t => t.AssignedDoctor).ThenInclude(d => d.VaccinationCenter_)
                .Include(a => a.Vaccine_).ThenInclude(v => v.Virus_);
        }

        public IEnumerable<Appointment> GetFormerAppointments(Guid patientId)
        {
            return _dbContext.Appointments.Where(a => a.Patient_.Id == patientId && a.TimeSlot_.From < DateTime.Now)
                .Include(a => a.TimeSlot_).ThenInclude(t => t.AssignedDoctor).ThenInclude(d => d.VaccinationCenter_)
                .Include(a => a.Vaccine_);
        }

        public void ConfirmVaccination(Guid appointmentId)
        {
            _dbContext.Appointments.Where(a => a.Id == appointmentId).ToList().ForEach(a => a.Status = AppointmentStatus.Finished);
            _dbContext.SaveChanges();
        }

        public void AddVaccinationBatchNumber(Guid appointmentId, string batchNumber)
        {
            _dbContext.Appointments.Where(a => a.Id == appointmentId).ToList().ForEach(a => a.VaccineBatchNumber = batchNumber);
            _dbContext.SaveChanges();
        }

        public void CreateCertificate(Certificate certificateToAdd)
        {
            _dbContext.Add(certificateToAdd);
            _dbContext.SaveChanges();
        }


        public IEnumerable<TimeSlot> FilterTimeslots(string city, DateTime dateFrom, DateTime dateTo, string virus)
        {
            return _dbContext.TimeSlots.Include(t => t.AssignedDoctor)
                .ThenInclude(d => d.VaccinationCenter_)
                .ThenInclude(vc => vc.AvailableVaccines)
                .ThenInclude(v => v.Virus_)
                .Include(t => t.AssignedDoctor).ThenInclude(d => d.VaccinationCenter_).ThenInclude(vc => vc.OpeningHours_).ThenInclude(oh => oh.MondayOpen)
                .Include(t => t.AssignedDoctor).ThenInclude(d => d.VaccinationCenter_).ThenInclude(vc => vc.OpeningHours_).ThenInclude(oh => oh.MondayClose)
                .Include(t => t.AssignedDoctor).ThenInclude(d => d.VaccinationCenter_).ThenInclude(vc => vc.OpeningHours_).ThenInclude(oh => oh.TuesdayOpen)
                .Include(t => t.AssignedDoctor).ThenInclude(d => d.VaccinationCenter_).ThenInclude(vc => vc.OpeningHours_).ThenInclude(oh => oh.TuesdayClose)
                .Include(t => t.AssignedDoctor).ThenInclude(d => d.VaccinationCenter_).ThenInclude(vc => vc.OpeningHours_).ThenInclude(oh => oh.WednesdayOpen)
                .Include(t => t.AssignedDoctor).ThenInclude(d => d.VaccinationCenter_).ThenInclude(vc => vc.OpeningHours_).ThenInclude(oh => oh.WednesdayClose)
                .Include(t => t.AssignedDoctor).ThenInclude(d => d.VaccinationCenter_).ThenInclude(vc => vc.OpeningHours_).ThenInclude(oh => oh.ThursdayOpen)
                .Include(t => t.AssignedDoctor).ThenInclude(d => d.VaccinationCenter_).ThenInclude(vc => vc.OpeningHours_).ThenInclude(oh => oh.ThursdayClose)
                .Include(t => t.AssignedDoctor).ThenInclude(d => d.VaccinationCenter_).ThenInclude(vc => vc.OpeningHours_).ThenInclude(oh => oh.FridayOpen)
                .Include(t => t.AssignedDoctor).ThenInclude(d => d.VaccinationCenter_).ThenInclude(vc => vc.OpeningHours_).ThenInclude(oh => oh.FridayClose)
                .Include(t => t.AssignedDoctor).ThenInclude(d => d.VaccinationCenter_).ThenInclude(vc => vc.OpeningHours_).ThenInclude(oh => oh.SaturdayOpen)
                .Include(t => t.AssignedDoctor).ThenInclude(d => d.VaccinationCenter_).ThenInclude(vc => vc.OpeningHours_).ThenInclude(oh => oh.SaturdayClose)
                .Include(t => t.AssignedDoctor).ThenInclude(d => d.VaccinationCenter_).ThenInclude(vc => vc.OpeningHours_).ThenInclude(oh => oh.SundayOpen)
                .Include(t => t.AssignedDoctor).ThenInclude(d => d.VaccinationCenter_).ThenInclude(vc => vc.OpeningHours_).ThenInclude(oh => oh.SundayClose)
                .Where(t => t.AssignedDoctor.VaccinationCenter_.City == city && t.From >= dateFrom
                && t.To <= dateTo && t.AssignedDoctor.VaccinationCenter_.AvailableVaccines.Select(v => v.Virus_.Name == virus).Count() > 0).ToList();
            // timeSlot isn't directly related to virus
        }

        public void SeedData()
        {
            _dbContext.VaccinationCenters.Add(new VaccinationCenter()
            {
                Active = true,
                Id = Guid.Parse("a3c2b53f-d1f9-4e45-a4d0-0732fe1179bd"),
                Address = "Hoża 15",
                City = "Warszawa",
                Name = "Centrum Szczepień na Hożej",
            });

            _dbContext.Patients.Add(new Patient()
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
            });

            _dbContext.Viruses.Add(new Virus()
            {
                Id = Guid.NewGuid(),
                Name = "SARS COVID-2019"
            });
            _dbContext.Vaccines.Add(new Vaccine()
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
                Virus_ = _dbContext.Viruses.Where(v => v.Name == "SARS COVID-2019").SingleOrDefault(),
            });
            var timeslotId1 = Guid.NewGuid();
            

            _dbContext.Appointments.Add(new Appointment()
            {
                Id = Guid.NewGuid(),
                Patient_ = _dbContext.Patients.Where(p => p.EMail == "rlewandowski@gmail.com").SingleOrDefault(),
                //PatientId = _dbContext.Patients.Where(p => p.EMail == "rlewandowski@gmail.com").SingleOrDefault().Id,
                Status = AppointmentStatus.Planned,
                VaccineBatchNumber = "24601",
                Vaccine_ = null,
                TimeslotId = timeslotId1,
                WhichDose = 1,
            });

            Guid doctorId1 = Guid.NewGuid();
            _dbContext.Doctors.Add(new Doctor()
            {
                Id = doctorId1,
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
            });

            _dbContext.TimeSlots.Add(new TimeSlot()
            {
                Id = timeslotId1,
                From = new DateTime(2022, 7, 3, 8, 0, 0),
                To = new DateTime(2022, 7, 3, 9, 0, 0),
                AssignedDoctorId = doctorId1,
                Active = true,
                IsFree = false,
            });

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

            _dbContext.OpeningHours.Add(new OpeningHours()
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
            });

            _dbContext.SaveChanges();

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
               ClosingHours = new TimeHours[] { new TimeHours(18, 0) },*/

       /*private readonly List<Patient> patients = new()
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


        }

        public bool CreatePatient(RegisterRequest registerRequest, Guid guid = default(Guid))
        {
            if (guid == default(Guid))
                guid = Guid.NewGuid();

           var result = _dbContext.Patients.Add(new Patient()
            {
                Active = true,
                DateOfBirth = registerRequest.DateOfBirth,
                EMail = registerRequest.Mail,
                FirstName = registerRequest.FirstName,
                LastName = registerRequest.LastName,
                PhoneNumber = registerRequest.PhoneNumber,
                Pesel = registerRequest.PESEL,
                Id = guid
           });

            int entries = _dbContext.SaveChanges();

            return entries > 0;
        }

        public bool EditPatient(PatientDTO patientToEdit, out bool patientFound)
        {
            var patientFromDb = _dbContext.Patients.Where(p => p.Id == Guid.Parse(patientToEdit.PatientId)).FirstOrDefault();
            if(patientToEdit is null)
            {
                patientFound = false;
                return false;
            }
            patientFound = true;

            patientFromDb.Pesel = patientToEdit.PESEL;
            patientFromDb.Active = patientToEdit.Active;
            patientFromDb.DateOfBirth = patientToEdit.DateOfBirth;
            patientFromDb.EMail = patientToEdit.Mail;
            patientFromDb.LastName = patientToEdit.LastName;
            patientFromDb.FirstName = patientToEdit.FirstName;
            patientFromDb.PhoneNumber = patientToEdit.PhoneNumber;

            int entitiesUpdated = _dbContext.SaveChanges();

            return entitiesUpdated > 0;
        }

        public bool RemovePatient(Guid patientId)
        {
            Patient patientToRemove = _dbContext.Patients.Where(p => p.Id == patientId).SingleOrDefault();
            _dbContext.Patients.Remove(patientToRemove);

            int entitiesModified = _dbContext.SaveChanges();
            return entitiesModified > 0;
        }

        public IEnumerable<Doctor> GetDoctorsWithMatchingVaccinationCentres()
        {
            return _dbContext.Doctors.Include(d => d.VaccinationCenter_).ToArray();
        }

        public bool EditDoctor(DoctorDTO doctorData, out bool doctorFound)
        {
            var doctorToEdit = _dbContext.Doctors.Where(d => d.Id == doctorData.Id).FirstOrDefault();
            if (doctorToEdit is null) 
            {
                doctorFound = false;
                return false;
            }

            doctorFound = true;

            doctorToEdit.Pesel = doctorData.PESEL;
            doctorToEdit.FirstName = doctorData.FirstName;
            doctorToEdit.LastName = doctorData.LastName;
            doctorToEdit.EMail = doctorData.Mail;
            doctorToEdit.DateOfBirth = doctorData.DateOfBirth;
            doctorToEdit.PhoneNumber = doctorData.PhoneNumber;
            doctorToEdit.Active = doctorData.Active;
            doctorToEdit.VaccinationCenterId = doctorData.VaccinationCenterID;

            int entitiesChanged = _dbContext.SaveChanges();
            return entitiesChanged > 0;
        }

        public bool AddDoctor(Doctor doctorToAdd)
        {
            int entitiesChanged = -1;
            try
            {
                _dbContext.Doctors.Add(doctorToAdd);
                entitiesChanged = _dbContext.SaveChanges();
            }
            catch(Exception ex)
            {
                return false;
            }

            return entitiesChanged > 0;
        }

        public bool DeleteDoctor(Guid doctorId, out bool wasDoctorFound)
        {
            var doctorToRemove = _dbContext.Doctors.Where(d => d.Id == doctorId).SingleOrDefault();

            if (doctorToRemove is null)
            {
                wasDoctorFound = false;
                return false;
            }
                wasDoctorFound = true;

            _dbContext.Doctors.Remove(doctorToRemove);

            int entitiesChanged = _dbContext.SaveChanges();

            return entitiesChanged > 0;
        }

        public IEnumerable<string> GetViruses()
        {
            var viruses = _dbContext.Viruses.ToArray();
            List<string> virusNames = new List<string>();
            foreach(var v in viruses)
            {
                virusNames.Add(v.Name);
            }
            return virusNames;
        }
        public IEnumerable<string> GetCities()
        {
            var vaccinationCenters = _dbContext.VaccinationCenters.ToArray();
            HashSet<string> cities = new HashSet<string>();

            foreach(var vc in vaccinationCenters)
            {
                cities.Add(vc.City);
            }

            return cities;
        }

        public IEnumerable<TimeSlot> GetDoctorsTimeSlots(Guid doctorId)
        {
            var timeslots = _dbContext.TimeSlots.Where(t => t.AssignedDoctorId == doctorId).ToArray();

            if (timeslots.Length == 0)
                throw new ModelNotFoundException();

            return timeslots;
        }

        public void DeleteTimeslots(IEnumerable<Guid> timeslotIds)
        {
            var timeslots = _dbContext.TimeSlots.Where(t => timeslotIds.Contains(t.Id));

            if (timeslots.Count() == 0)
                throw new ModelNotFoundException();

            foreach(var t in timeslots)
            {
                t.Active = false;
            }

            _dbContext.SaveChanges();
        }

        public IEnumerable<VaccinationCenter> GetVaccinationCenters()
        {
            var vaccinationCenters = _dbContext.VaccinationCenters.ToArray();

            if (vaccinationCenters.Length == 0)
                throw new ModelNotFoundException();

            return vaccinationCenters;
        }

        public void AddVaccinationCenter(AddVaccinationCenterRequest centerToAdd)
        {
            VaccinationCenter vaccinationCenter = new VaccinationCenter();
            vaccinationCenter.Id = Guid.NewGuid();
            vaccinationCenter.Name = centerToAdd.Name;
            vaccinationCenter.City = centerToAdd.City;
            vaccinationCenter.Address = centerToAdd.Street;
            vaccinationCenter.Active = centerToAdd.Active;
            vaccinationCenter.OpeningHours_ = _timeHoursService.DTOToOpeningHours(centerToAdd.OpeningHoursDays as IList<OpeningHoursDTO>);


            _dbContext.VaccinationCenters.Add(vaccinationCenter);

            int entitiesChanged = _dbContext.SaveChanges();

            if(entitiesChanged <= 0)
                throw new NoChangesInDatabaseException();
        }

        public void EditVaccinationCenter(VaccinationCenterDTO centerToEdit)
        {
            var vaccinationCenterFromTheDatabase = _dbContext.VaccinationCenters.Where(vc => vc.Id == centerToEdit.Id).Single();

            vaccinationCenterFromTheDatabase.Name = centerToEdit.Name;
            vaccinationCenterFromTheDatabase.City = centerToEdit.City;
            vaccinationCenterFromTheDatabase.Address = centerToEdit.Street;
            vaccinationCenterFromTheDatabase.AvailableVaccines = new List<Vaccine>();

            foreach(var v in centerToEdit.Vaccines)
            {
                vaccinationCenterFromTheDatabase.AvailableVaccines.Add(new Vaccine() { Id = v.Id });
            }

            vaccinationCenterFromTheDatabase.OpeningHours_ = _timeHoursService.DTOToOpeningHours(centerToEdit.OpeningHoursDays as  IList<OpeningHoursDTO>);
            vaccinationCenterFromTheDatabase.Active = centerToEdit.Active;

            _dbContext.SaveChanges();
        }

        public void DeleteVaccinationCenter(Guid centerId)
        {
            var vaccinationCenter = _dbContext.VaccinationCenters.Where(vc => vc.Id == centerId).SingleOrDefault();
            _dbContext.Remove(vaccinationCenter);
            int entitiesChanged = _dbContext.SaveChanges();

            if (entitiesChanged <= 0)
                throw new NoChangesInDatabaseException();
        }
    }
}
