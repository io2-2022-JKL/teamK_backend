using Microsoft.AspNetCore.Mvc;
using System;
using VaccinationSystemApi.Dtos.Doctors;
using VaccinationSystemApi.Repositories.Interfaces;
using VaccinationSystemApi.Helpers;
using VaccinationSystemApi.Models;
using System.Collections;
using System.Collections.Generic;
using VaccinationSystemApi.Helpers.Converters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using VaccinationSystemApi.Models.Utils;
using VaccinationSystemApi.Services;


namespace VaccinationSystemApi.Controllers
{

    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Doctor")]
    public class DoctorController: ControllerBase
    {
        private readonly IVaccinationSystemRepository _vaccinationService;
        private readonly MailService _mailService;
        public DoctorController(IVaccinationSystemRepository repo, MailService mailService)
        {
            _vaccinationService = repo;
            _mailService = mailService;
        }

        [HttpGet("doctor/info/{doctorId}")]
        public ActionResult<GetDoctorInfoResponse> GetDoctorInfo(Guid doctorId)
        {
            try
            {
                var doctorFromDb = _vaccinationService.GetDoctor(doctorId);
                if (doctorFromDb is null)
                    return NotFound();

                var centerFromDb = _vaccinationService.GetCenter(doctorFromDb.VaccinationCenterId);

                GetDoctorInfoResponse response = new()
                {
                    PatientAccountId = doctorFromDb.PatientAccountId.ToString(),
                    VaccinationCenterId = doctorFromDb.VaccinationCenterId.ToString(),
                    VaccinationCenterName = centerFromDb.Name,
                    VaccinationCenterStreet = centerFromDb.Address,
                    VaccinationCenterCity = centerFromDb.City,
                };

                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet("doctor/timeSlots/{doctorId}")]
        public ActionResult<IEnumerable<GetTimeSlotsResponse>> GetTimeSlots(Guid doctorId)
        {
            try
            {
                var doctorFromDb = _vaccinationService.GetDoctor(doctorId);
                if (doctorFromDb is null)
                    return BadRequest();

                var timeSlotsFromDb = _vaccinationService.GetDoctorTimeSlots(doctorId);
                List<GetTimeSlotsResponse> response = new List<GetTimeSlotsResponse>();
                foreach (var timeSlot in timeSlotsFromDb)
                {
                    response.Add(ConvertTimeSlotToResponse.Convert(timeSlot));
                }
                if (response.Count == 0)
                    return NotFound();

                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet("doctor/formerAppointments/{doctorId}")]
        public ActionResult<IEnumerable<GetFormerAppointmentsResponse>> GetFormerAppointments(Guid doctorId)
        {
            try
            {
                var doctorFromDb = _vaccinationService.GetDoctor(doctorId);
                if (doctorFromDb is null)
                    return BadRequest();

                List<GetFormerAppointmentsResponse> response = new List<GetFormerAppointmentsResponse>();
                var appointmentsFromDb = _vaccinationService.GetDoctorsFormerAppointments(doctorId);

                foreach (var appointment in appointmentsFromDb)
                {
                    if (appointment.TimeSlot_.AssignedDoctorId == doctorId)
                    {
                        if (appointment.Patient_ is null) continue;

                        response.Add(new GetFormerAppointmentsResponse()
                        {
                            PatientFirstName = appointment.Patient_.FirstName,
                            PatientLastName = appointment.Patient_.LastName,
                            AppointmentId = appointment.Id.ToString(),
                            BatchNumber = appointment.VaccineBatchNumber,
                            CertifyState = "",
                            From = appointment.TimeSlot_.From.ToString("dd-MM-yyyy HH:mm"),
                            To = appointment.TimeSlot_.To.ToString("dd-MM-yyyy HH:mm"),
                            Pesel = appointment.Patient_.Pesel,
                            State = appointment.Status.ToString(),
                            VaccineCompany = appointment.Vaccine_.Company,
                            VaccineDose = appointment.WhichDose,
                            VaccineName = appointment.Vaccine_.Name,
                            VaccineVirus = appointment.Vaccine_.Virus_.Name,
                        });

                    }
                }

                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet("doctor/incomingAppointments/{doctorId}")]
        public ActionResult<IEnumerable<GetIncomingAppointmentsResponse>> GetIncomingAppointments(Guid doctorId)
        {
            try
            {
                var doctorFromDb = _vaccinationService.GetDoctor(doctorId);
                if (doctorFromDb is null)
                    return BadRequest();

                List<GetIncomingAppointmentsResponse> response = new List<GetIncomingAppointmentsResponse>();
                var appointmentsFromDb = _vaccinationService.GetDoctorsIncomingAppointments(doctorId);

                foreach (var appointment in appointmentsFromDb)
                {
                    if (appointment.Patient_ is null) continue;

                    response.Add(new GetIncomingAppointmentsResponse()
                    {
                        appointmentId = appointment.Id.ToString(),
                        from = appointment.TimeSlot_.From.ToString("dd-MM-yyyy HH:mm"),
                        to = appointment.TimeSlot_.To.ToString("dd-MM-yyyy HH:mm"),
                        patientFirstName = appointment.Patient_.FirstName,
                        patientLastName = appointment.Patient_.LastName,
                        vaccineCompany = appointment.Vaccine_.Company,
                        vaccineName = appointment.Vaccine_.Name,
                        vaccineVirus = appointment.Vaccine_.Virus_.Name,
                        whichVaccineDose = appointment.WhichDose
                    });
                }
                if (response.Count == 0)
                    return NotFound("Doctor has no incoming appointments so far");

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet("doctor/vaccinate/{doctorId}/{appointmentId}")]
        public ActionResult<GetVaccinateInfoResponse> GetVaccinateInfo(Guid doctorId, Guid appointmentId)
        {
            try
            {
                if (_vaccinationService.GetDoctor(doctorId) is null)
                    return BadRequest("Doctor with given id doesn't exist");
                var appointmentsFromDb = _vaccinationService.GetNotCancelledAppointments();
                List<Appointment> doctorAppointments = new List<Appointment>();
                foreach (var appointment in appointmentsFromDb)
                {
                    var slotFromDb = _vaccinationService.GetTimeSlot(appointment.TimeslotId);
                    if (slotFromDb.AssignedDoctorId == doctorId && appointmentId == appointment.Id)
                    {
                        GetVaccinateInfoResponse response = new()
                        {
                            dateOfBirth = appointment.Patient_.DateOfBirth.ToString(),
                            from = slotFromDb.From.ToString(),
                            maxDaysBetweenDoses = appointment.Vaccine_.MaxDaysBetweenDoses,
                            maxPatientAge = appointment.Vaccine_.MaxPatientAge,
                            minDaysBetweenDoses = appointment.Vaccine_.MinDaysBetweenDoses,
                            minPatientAge = appointment.Vaccine_.MinPatientAge,
                            numberOfDoses = appointment.Vaccine_.NumberOfDoses,
                            patientFirstName = appointment.Patient_.FirstName,
                            patientLastName = appointment.Patient_.LastName,
                            pesel = appointment.Patient_.Pesel,
                            to = slotFromDb.To.ToString(),
                            vaccineCompany = appointment.Vaccine_.Company,
                            vaccineName = appointment.Vaccine_.Name,
                            virusName = appointment.Vaccine_.Virus_.Name,
                        };

                        return Ok(response);
                    }
                }
                return NotFound("No appointment with given id found for given doctor");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("doctor/timeSlots/create/{doctorId}")]
        public ActionResult CreateTimeSlot(Guid doctorId, CreateTimeSlotRequest request)
        {
            try
            {
                var dateStart = DateTime.ParseExact(request.windowBegin, "dd-MM-yyyy HH:mm", null);
                var timeSpan = TimeSpan.FromMinutes(request.timeSlotDurationInMinutes);
                var slotsFromDb = _vaccinationService.GetDoctorActiveSlots(doctorId, dateStart.Date);
                if (slotsFromDb is null)
                {
                    _vaccinationService.CreateTimeSlot(new TimeSlot
                    {
                        Active = true,
                        AssignedDoctorId = doctorId,
                        From = dateStart,
                        To = dateStart + timeSpan,
                        IsFree = true,
                        Id = Guid.NewGuid()
                    });
                    return Ok();
                }
                while (dateStart + timeSpan <= DateTime.ParseExact(request.windowEnd, "dd-MM-yyyy HH:mm", null))
                {
                    TimeSlotValidator.Validate(dateStart, dateStart + timeSpan);
                    bool isFree = true;
                    foreach (var existingSlots in slotsFromDb)
                    {
                        if (!TimeSlotValidator.IsAvailable(dateStart, dateStart + timeSpan, existingSlots.From, existingSlots.To))
                        {
                            isFree = false;
                            break;
                        }
                    }

                    if (isFree)
                    {
                        _vaccinationService.CreateTimeSlot(new TimeSlot
                        {
                            Active = true,
                            AssignedDoctorId = doctorId,
                            From = dateStart,
                            To = dateStart + timeSpan,
                            IsFree = true,
                            Id = Guid.NewGuid()
                        });
                    }
                    dateStart += timeSpan;
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("doctor/timeSlots/delete/{doctorId}")]
        public ActionResult DeleteTimeSlots(Guid doctorId, IEnumerable<DeleteTimeSlotsRequest> request)
        {
            try
            {
                foreach (var slot in request)
                {
                    if (_vaccinationService.GetDoctorByTimeSlot(slot.id).Id == doctorId)
                    {
                        _vaccinationService.DeleteTimeSlot(slot.id);
                    }
                }
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost("doctor/timeSlots/modify/{doctorId}/{timeSlotId}")]
        public ActionResult ModifyTimeSlot(Guid doctorId, Guid timeSlotId, ModifyTimeSlotRequest request)
        {
            try
            {
                if (_vaccinationService.GetDoctorByTimeSlot(timeSlotId).Id == doctorId)
                {
                    _vaccinationService.ModifyTimeSlot(timeSlotId,
                        DateTime.ParseExact(request.TimeFrom, "dd-MM-yyyy HH:mm", null),
                        DateTime.ParseExact(request.TimeTo, "dd-MM-yyyy HH:mm", null));

                    return Ok();
                }

                return NotFound();
            }
            catch(Exception ex)
            {
                return BadRequest();
            }

        }

        [HttpPost("doctor/vaccinate/confirmVaccination/{doctorId}/{appointmentId}/{batchId}")]

        public ActionResult<ConfirmVaccinationResponse> ConfirmVaccination(Guid doctorId, Guid appointmentId, string batchId)
        {
            try
            {
                var appointmentFromDb = _vaccinationService.GetAppointment(appointmentId);
                if (appointmentFromDb is null)
                    return BadRequest("Appointment with given id doesn't exist");

                if (appointmentFromDb.TimeSlot_.AssignedDoctor.Id != doctorId)
                    return Forbid("User forbidden to confirm vaccination");

                /*if (appointmentFromDb.TimeSlot_.From >= DateTime.UtcNow)
                    return Forbid("This appointment is in the future");*/

                _vaccinationService.ConfirmVaccination(appointmentId);
                var patientFormerAppointmentsFromDb = _vaccinationService.GetPatientsFormerAppointments(appointmentFromDb.Patient_.Id);

                int finishedDoses = 0;
                foreach (var appointment in patientFormerAppointmentsFromDb)
                {
                    if (appointment.Vaccine_.Id == appointmentFromDb.Vaccine_.Id && appointment.Status == AppointmentStatus.Finished)
                        finishedDoses++;
                }
                _vaccinationService.AddVaccinationBatchNumber(appointmentId, batchId);
                bool isLastDose = finishedDoses >= appointmentFromDb.Vaccine_.NumberOfDoses;
                ConfirmVaccinationResponse response = new()
                {
                    canCertify = isLastDose,
                };

                _mailService.SendConfirmVaccinationMail(appointmentFromDb.Patient_.EMail);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost("doctor/vaccinate/vaccinationDidNotHappen/{doctorId}/{appointmentId}")]

        public ActionResult CancelVaccination(Guid doctorId, Guid appointmentId)
        {
            try
            {
                var appointmentFromDb = _vaccinationService.GetAppointment(appointmentId);

                if (appointmentFromDb is null)
                    return BadRequest("Appointment with given id doesn't exist");

                if (appointmentFromDb.TimeSlot_.AssignedDoctor.Id != doctorId)
                    return Forbid("User forbidden to confirm vaccination");

                _vaccinationService.CancelAppointment(appointmentId);
                _mailService.SendCancelVaccinationMail(appointmentFromDb.Patient_.EMail);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost("doctor/vaccinate/certify/{doctorId}/{appointmentId}")]
        public ActionResult Certify(Guid doctorId, Guid appointmentId)
        {
            try
            {
                var appointmentFromDb = _vaccinationService.GetAppointment(appointmentId);
                if (appointmentFromDb is null)
                    return BadRequest("Appointment with given id doesn't exist");

                if (appointmentFromDb.TimeSlot_.AssignedDoctor.Id != doctorId)
                    return Forbid("User forbidden to confirm vaccination");

                int finishedDoses = 0;
                var patientFormerAppointmentsFromDb = _vaccinationService.GetPatientsFormerAppointments(appointmentFromDb.Patient_.Id);
                foreach (var appointment in patientFormerAppointmentsFromDb)
                {
                    if (appointment.Vaccine_.Id == appointmentFromDb.Vaccine_.Id && appointment.Status == AppointmentStatus.Finished)
                        finishedDoses++;
                }
                bool canBeCertified = finishedDoses >= appointmentFromDb.Vaccine_.NumberOfDoses;
                if (!canBeCertified)
                    return BadRequest("That wasn't the lat dose");

                _vaccinationService.CreateCertificate(new Certificate()
                {
                    Id = Guid.NewGuid(),
                    Owner = appointmentFromDb.Patient_,
                    Url = ""
                });
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }

    }
}
