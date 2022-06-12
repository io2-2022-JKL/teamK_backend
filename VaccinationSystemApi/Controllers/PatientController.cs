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
    [Route("patient")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Patient")]
    public class PatientController : ControllerBase
    {
        private readonly IVaccinationSystemRepository _vaccinationService;
        private readonly TimeHoursService _timeHoursService;

        public PatientController(IVaccinationSystemRepository repo)
        {
            _vaccinationService = repo;
            _timeHoursService = new TimeHoursService();
            
        }

        [HttpGet("info/{patientId}")]
        public ActionResult<BrowsePatientByIdResponse> GetPatientInfo(Guid patientId)
        {
            try
            {
                var patientFromDb = _vaccinationService.GetPatient(patientId);
                if (patientFromDb is null)
                    return NotFound();
                BrowsePatientByIdResponse patientResponse = new()
                {
                    dateOfBirth = patientFromDb.DateOfBirth.ToString("dd-MM-yyyy HH:mm"),
                    mail = patientFromDb.EMail,
                    firstName = patientFromDb.FirstName,
                    lastName = patientFromDb.LastName,
                    PESEL = patientFromDb.Pesel,
                    phoneNumber = patientFromDb.PhoneNumber
                };

                return Ok(patientResponse);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /*[HttpGet("centers/{city}")]
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
        }*/

        [HttpGet("certificates/{patientId}")]
        public ActionResult<IEnumerable<BrowseCertificateResponse>> GetPatientCertificates(Guid patientId)
        {
                var patientFromDb = _vaccinationService.GetPatient(patientId);
                if (patientFromDb is null)
                    return BadRequest();

                var cert = _vaccinationService.GetPatientCertificates(patientId);

                if (cert.Count() == 0)
                    return NotFound();

                List<BrowseCertificateResponse> response = new List<BrowseCertificateResponse>();
                foreach(var c in cert)
                {
                    response.Add(new()
                    {
                        Url = c.Url,
                        VaccineCompany = c.Vaccine_.Company,
                        VaccineName = c.Vaccine_.Name,
                        VirusType = c.Vaccine_.Virus_.Name
                    });
                }
                return Ok(response);
        }

       /* [HttpGet("timeSlots/{id}")]
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
        }*/

        [HttpPost("timeSlots/Book/{patientId}/{timeSlotId}/{vaccineId}")]
        public ActionResult MakeAppointment(Guid patientId, Guid timeSlotId, Guid vaccineId)
        {
            var slotFromDb = _vaccinationService.GetTimeSlot(timeSlotId);
            if (slotFromDb is null)
                return NotFound();
            if (slotFromDb.IsFree == false || slotFromDb.Active == false)
                return Forbid();
            try
            {
                var appointmentId = _vaccinationService.CreateAppointment(patientId, timeSlotId, vaccineId);
                _vaccinationService.ReserveTimeSlot(timeSlotId);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpDelete("appointments/incomingAppointments/cancelAppointments/{patientId}/{id}")]
        public ActionResult CancelAppointment(Guid id)
        {
            try
            {
                var appointmentFromDb = _vaccinationService.GetAppointment(id);

                if (appointmentFromDb is null)
                    return NotFound("Error, incoming appointment has not been found");

                _vaccinationService.CancelAppointment(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet("appointments/incomingAppointments/{patientId}")]
        public ActionResult<ICollection<AppointmentResponse>> GetIncomingAppointments(Guid patientId)
        {
            try
            {
                var incomingAppointments = _vaccinationService.GetPatientsIncomingAppointments(patientId);
                var appointmentResponses = new List<AppointmentResponse>();

                foreach (var app in incomingAppointments)
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
                        WindowBegin = app.TimeSlot_.From.ToString("dd-MM-yyyy HH:mm"),
                        WindowEnd = app.TimeSlot_.To.ToString("dd-MM-yyyy HH:mm"),
                    };
                    appointmentResponses.Add(appointmentDTO);
                }
                return Ok(appointmentResponses);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet("appointments/formerAppointments/{patientId}")]
        public ActionResult<ICollection<AppointmentResponse>> GetFormerAppointments(Guid patientId)
        {
            try
            {
                var formerAppointments = _vaccinationService.GetPatientsFormerAppointments(patientId);
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
                        WindowBegin = app.TimeSlot_.From.ToString("dd-MM-yyyy HH:mm"),
                        WindowEnd = app.TimeSlot_.To.ToString("dd-MM-yyyy HH:mm"),
                    };
                    appointmentResponses.Add(appointmentDTO);
                }
                return appointmentResponses;
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet("timeSlots/filter")]
        public ActionResult<ICollection<FilterTimeslotsResponse>> FilterTimeslots(string city, string dateFrom, string dateTo, string virus)
        {
            IEnumerable <TimeSlot> timeslotsFromDb;
            try
            {
                timeslotsFromDb = _vaccinationService.FilterTimeslots(city, DateTime.ParseExact(dateFrom, "dd-MM-yyyy HH:mm", null), DateTime.ParseExact(dateTo, "dd-MM-yyyy HH:mm", null), virus);
                if (timeslotsFromDb.Count() == 0)
                    return NotFound();
            
            }
            catch (Exception)
            {
                return BadRequest();
            }

            var responses = new List<FilterTimeslotsResponse>();

            foreach(var t in timeslotsFromDb)
            {
                var vaccines = t.AssignedDoctor.VaccinationCenter_.AvailableVaccines;

                var filterTimeslotResponse = new FilterTimeslotsResponse()
                {
                    AvailableVaccines = VaccineCollectionToDTO(t.AssignedDoctor.VaccinationCenter_.AvailableVaccines),
                    DoctorFirstName = t.AssignedDoctor.FirstName,
                    DoctorLastName = t.AssignedDoctor.LastName,
                    From = t.From.ToString("dd-MM-yyyy HH:mm"),
                    To = t.To.ToString("dd-MM-yyyy HH:mm"),
                    openingHours = _timeHoursService.OpeningHoursToDTO(t.AssignedDoctor.VaccinationCenter_.OpeningHours_),
                    TimeSlotId = t.Id.ToString(),
                    VaccinationCenterCity = t.AssignedDoctor.VaccinationCenter_.City,
                    VaccinationCenterName = t.AssignedDoctor.VaccinationCenter_.Name,
                    VaccinationCenterStreet = t.AssignedDoctor.VaccinationCenter_.Address,
                };
                responses.Add(filterTimeslotResponse);
            }
            return Ok(responses);
        }

        private ICollection<VaccineDTO> VaccineCollectionToDTO(ICollection<Vaccine> vaccines)
        {
            var result = new List<VaccineDTO>();
            foreach (var v in vaccines)
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
