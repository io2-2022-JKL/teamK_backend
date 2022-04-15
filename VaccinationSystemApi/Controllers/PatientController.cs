using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using VaccinationSystemApi.Models;
using VaccinationSystemApi.Repositories.Interfaces;
using VaccinationSystemApi.Dtos.Patients;
using VaccinationSystemApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace VaccinationSystemApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PatientController : ControllerBase
    {
        private readonly IVaccinationSystemRepository _vaccinationService;
        private readonly TimeHoursService _timeHoursService;

        public PatientController(IVaccinationSystemRepository repo)
        {
            _vaccinationService = repo;
            _timeHoursService = new TimeHoursService();
            
        }

        [HttpGet("centers/{city}")]
        public IEnumerable<BrowseVaccinationCentersResponse> BrowseVaccinationCenters(string city)
        {
            
            List<BrowseVaccinationCentersResponse> centers = new List<BrowseVaccinationCentersResponse>();
            foreach(var s in _vaccinationService.GetCenters().Where(x => x.City == city))
            {
                centers.Add(new BrowseVaccinationCentersResponse
                {
                    Active = s.Active,
                    Address = s.Address,
                    AvailableVaccines = s.AvailableVaccines,
                    City = s.City,
                    Doctors = s.Doctors,
                    Id = s.Id,
                    Name = s.Name,
                    OpeningHours = null
                }) ;
            };

            return centers;
        }

        [HttpGet("timeSlots/{id}")]
        public IEnumerable<TimeHoursResponse> GetTimeSlots(Guid id, DateTime date)
        {
            var slotsFromDb = _vaccinationService.GetTimeSlots();
            List<TimeHoursResponse> searched = new List<TimeHoursResponse>();

            foreach(var slot in slotsFromDb)
            {
                var center = _vaccinationService.GetCenterOfDoctor(slot.AssignedDoctorId);
                var slotDate = slot.From.Date;
                if (DateTime.Compare(date, slotDate) == 0 && center.Id == id)
                    searched.Add(new TimeHoursResponse
                    {
                        Id = slot.Id,
                        From = slot.From,
                        To = slot.To,
                    });
            };

            return searched;
        }

        [HttpPost("appointments/create/{patientId}/{timeSlotId}/{vaccineId}")]
        public Guid MakeAppointment(Guid patientId, Guid timeSlotId, Guid vaccineId)
        {
            return _vaccinationService.CreateAppointment(patientId, timeSlotId, vaccineId);
        }

        [HttpPost("appointments/cancel/{id}")]
        public void CancelAppointment(Guid id)
        {
            var appointmentFromDb = _vaccinationService.GetAppointment(id);
            if (appointmentFromDb is null) return;

            _vaccinationService.CancelAppointment(id);
        }

        [HttpGet("appointments/incomingAppointments/{patientId}")]
        public ICollection<AppointmentResponse> GetIncomingAppointments(Guid patientId)
        {
            var incomingAppointments = _vaccinationService.GetIncomingAppointments(patientId);
            var appointmentResponses = new List<AppointmentResponse>();

            foreach(var app in incomingAppointments)
            {
                var appointmentDTO = new AppointmentResponse()
                {
                    AppointmentId = app.Id.ToString(),
                    DoctorFirstName = app.TimeSlot_.AssignedDoctor.FirstName,
                    DoctorLastName = app.TimeSlot_.AssignedDoctor.LastName,
                    VaccinationCenterCity = app.TimeSlot_.AssignedDoctor.VaccinationCenter_.City,
                    VaccinationCenterName = app.TimeSlot_.AssignedDoctor.VaccinationCenter_.Name,
                    VaccinationCenterStreet = app.TimeSlot_.AssignedDoctor.VaccinationCenter_.Address,
                    VaccineCompany = app.Vaccine_.Company,
                    VaccineName = app.Vaccine_.Name,
                    VaccineVirus = app.Vaccine_.Virus_.Name,
                    WhichVaccineDose = app.WhichDose,
                    WindowBegin = app.TimeSlot_.From,
                    WindowEnd = app.TimeSlot_.To,
                };
                appointmentResponses.Add(appointmentDTO);
            }
            return appointmentResponses;
        }

        [HttpGet("appointments/formerAppointments/{patientId}")]
        public ICollection<AppointmentResponse> GetFormerAppointments(Guid patientId)
        {
            var formerAppointments = _vaccinationService.GetFormerAppointments(patientId);
            var appointmentResponses = new List<AppointmentResponse>();

            foreach (var app in formerAppointments)
            {
                var appointmentDTO = new AppointmentResponse()
                {
                    AppointmentId = app.Id.ToString(),
                    DoctorFirstName = app.TimeSlot_.AssignedDoctor.FirstName,
                    DoctorLastName = app.TimeSlot_.AssignedDoctor.LastName,
                    VaccinationCenterCity = app.TimeSlot_.AssignedDoctor.VaccinationCenter_.City,
                    VaccinationCenterName = app.TimeSlot_.AssignedDoctor.VaccinationCenter_.Name,
                    VaccinationCenterStreet = app.TimeSlot_.AssignedDoctor.VaccinationCenter_.Address,
                    VaccineCompany = app.Vaccine_.Company,
                    VaccineName = app.Vaccine_.Name,
                    VaccineVirus = app.Vaccine_.Virus_.Name,
                    WhichVaccineDose = app.WhichDose,
                    WindowBegin = app.TimeSlot_.From,
                    WindowEnd = app.TimeSlot_.To,
                };
                appointmentResponses.Add(appointmentDTO);
            }
            return appointmentResponses;
        }

        [HttpGet("/patient/timeSlots/Filter")]
        public ICollection<FilterTimeslotsResponse> FilterTimeslots(string city, DateTime dateFrom, DateTime dateTo, string virus)
        {
            var timeslotsFromDb = _vaccinationService.FilterTimeslots(city, dateFrom, dateTo, virus);
            var responses = new List<FilterTimeslotsResponse>();

            foreach(var t in timeslotsFromDb)
            {
                var vaccines = t.AssignedDoctor.VaccinationCenter_.AvailableVaccines;

                var filterTimeslotResponse = new FilterTimeslotsResponse()
                {
                    AvailableVaccines = VaccineCollectionToDTO(t.AssignedDoctor.VaccinationCenter_.AvailableVaccines),
                    DoctorFirstName = t.AssignedDoctor.FirstName,
                    DoctorLastName = t.AssignedDoctor.LastName,
                    From = t.From,
                    To = t.To,
                    openingHours = _timeHoursService.OpeningHoursToDTO(t.AssignedDoctor.VaccinationCenter_.OpeningHours_),
                    TimeSlotId = t.Id.ToString(),
                    VaccinationCenterCity = t.AssignedDoctor.VaccinationCenter_.City,
                    VaccinationCenterName = t.AssignedDoctor.VaccinationCenter_.Name,
                    VaccinationCenterStreet = t.AssignedDoctor.VaccinationCenter_.Address,
                };
                responses.Add(filterTimeslotResponse);
            }
            return responses;
        }

        private ICollection<VaccineDTO> VaccineCollectionToDTO(ICollection<Vaccine> vaccines)
        {
            var result = new List<VaccineDTO>();    
            foreach(var v in vaccines)
            {
                result.Add(VaccineToDTO(v));
            }
            return result;
        }
        private VaccineDTO VaccineToDTO(Vaccine vaccine)
        {
            return new VaccineDTO()
            {
                Company = vaccine.Company,
                MaxDaysBetweenDoses = vaccine.MaxDaysBetweenDoses,
                MaxPatientAge = vaccine.MaxPatientAge,
                MinDaysBetweenDoses = vaccine.MinDaysBetweenDoses,
                MinPatientAge = vaccine.MinPatientAge,
                Name = vaccine.Name,
                NumberOfDoses = vaccine.NumberOfDoses,
                VaccineId = vaccine.Id.ToString(),
                Virus = vaccine.Virus_.Name
            };
        }

        //this will require a separate service in the future
        

    }
}
