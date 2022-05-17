﻿using Microsoft.AspNetCore.Mvc;
using System;
using VaccinationSystemApi.Dtos.Doctors;
using VaccinationSystemApi.Repositories.Interfaces;
using VaccinationSystemApi.Helpers;
using VaccinationSystemApi.Models;
using System.Collections;
using System.Collections.Generic;
using VaccinationSystemApi.Helpers.Converters;

namespace VaccinationSystemApi.Controllers
{

    [ApiController]
    public class DoctorController: ControllerBase
    {
        private readonly IVaccinationSystemRepository _vaccinationService;
        public DoctorController(IVaccinationSystemRepository repo) => _vaccinationService = repo;

        [HttpGet("doctor/info/{doctorId}")]
        public ActionResult<GetDoctorInfoResponse> GetDoctorInfo(Guid doctorId)
        {
            var doctorFromDb = _vaccinationService.GetDoctor(doctorId);
            if (doctorFromDb is null)
                return NotFound();

            var centerFromDb = _vaccinationService.GetCenter(doctorFromDb.VaccinationCenterId);

           GetDoctorInfoResponse response = new()
            {
                PatientAccountId = "",
                VaccinationCenterId = doctorFromDb.VaccinationCenterId.ToString(),
                VaccinationCenterName = centerFromDb.Name,
                VaccinationCenterStreet = centerFromDb.Address,
            };

            return Ok(response);
        }

        [HttpGet("doctor/timeSlots/{doctorId}")]
        public ActionResult<IEnumerable<GetTimeSlotsResponse>> GetTimeSlots(Guid doctorId)
        {
            var doctorFromDb = _vaccinationService.GetDoctor(doctorId);
            if (doctorFromDb is null)
                return BadRequest();

            var timeSlotsFromDb = _vaccinationService.GetDoctorTimeSlots(doctorId);
            List<GetTimeSlotsResponse> response = new List<GetTimeSlotsResponse>();
            foreach(var timeSlot in timeSlotsFromDb)
            {
                response.Add(ConvertTimeSlotToResponse.Convert(timeSlot));
            }
            if (response.Count == 0)
                return NotFound();

            return Ok(response);
        }

        [HttpGet("doctor/formerAppointents/{doctorId}")]
        public ActionResult<IEnumerable<GetFormerAppointmentsResponse>> GetFormerAppointments(Guid doctorId)
        {
            var doctorFromDb = _vaccinationService.GetDoctor(doctorId);
            if (doctorFromDb is null)
                return BadRequest();

            List<GetFormerAppointmentsResponse> response = new List<GetFormerAppointmentsResponse>();
            var appointmentsFromDb = _vaccinationService.GetAppointments();

            foreach (var appointment in appointmentsFromDb)
            {
                var slotFromDb = _vaccinationService.GetTimeSlot(appointment.TimeslotId);
                if (slotFromDb.AssignedDoctorId == doctorId)
                {
                    
                    if(slotFromDb.To < DateTime.UtcNow)
                    {
                        if (appointment.Patient_ is null) continue;
                        response.Add(new GetFormerAppointmentsResponse()
                        {
                            PatientFirstName = appointment.Patient_.FirstName,
                            PatientLastName = appointment.Patient_.LastName,
                            AppointmentId = appointment.Id.ToString(),
                            BatchNumber = appointment.VaccineBatchNumber,
                            CertifyState = "",
                            From = slotFromDb.From.ToString(),
                            To = slotFromDb.To.ToString(),
                            Pesel = appointment.Patient_.Pesel,
                            State = "",
                            VaccineCompany = appointment.Vaccine_.Company,
                            VaccineDose = appointment.WhichDose,
                            VaccineName = appointment.Vaccine_.Name,
                            VaccineVirus = appointment.Vaccine_.Virus_.Name,
                        });
                    }
                }
            }

            return Ok(response);
        }

        [HttpGet("doctor/incomingAppointents/{doctorId}")]
        public ActionResult<IEnumerable<GetIncomingAppointmentsResponse>> GetIncomingAppointments(Guid doctorId)
        {
            var doctorFromDb = _vaccinationService.GetDoctor(doctorId);
            if (doctorFromDb is null)
                return BadRequest();

            List<GetIncomingAppointmentsResponse> response = new List<GetIncomingAppointmentsResponse>();
            var appointmentsFromDb = _vaccinationService.GetAppointments();

            foreach (var appointment in appointmentsFromDb)
            {
                var slotFromDb = _vaccinationService.GetTimeSlot(appointment.TimeslotId);
                if (slotFromDb.AssignedDoctorId == doctorId)
                {
                    //var patientFromDb = _vaccinationService.GetPatient(appointment.)

                    if (slotFromDb.To > DateTime.UtcNow)
                    {
                        if (appointment.Patient_ is null) continue;
                        response.Add(new GetIncomingAppointmentsResponse()
                        {
                            appointmentId = appointment.Id.ToString(),
                            from = slotFromDb.From.ToString(),
                            to = slotFromDb.To.ToString(),
                            patientFirstName = appointment.Patient_.FirstName,
                            patientLastName = appointment.Patient_.LastName,
                            vaccineCompany = appointment.Vaccine_.Company,
                            vaccineName = appointment.Vaccine_.Name,
                            vaccineVirus = appointment.Vaccine_.Virus_.Name,
                            whichVaccineDose = appointment.WhichDose
                        });
                    }
                }
            }
            if (response.Count == 0)
                return NotFound("Doctor has no incoming appointments so far");
            return Ok(response);
        }

        [HttpGet("doctor/vaccinate/{doctorId}/{appointmentId}")]
        public ActionResult<GetVaccinateInfoResponse> GetVaccinateInfo(Guid doctorId, Guid appointmentId)
        {
            if (_vaccinationService.GetDoctor(doctorId) is null)
                return BadRequest("Doctor with given id doesn't exist");
            var appointmentsFromDb = _vaccinationService.GetAppointments();
            List<Appointment> doctorAppointments = new List<Appointment>();
            foreach(var appointment in appointmentsFromDb)
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

        [HttpPost("doctor/timeSlot/create/{doctorId}")]
        public void CreateTimeSlot(Guid doctorId, CreateTimeSlotRequest request)
        {
            var dateStart = request.From;
            var timeSpan = TimeSpan.FromMinutes(request.Duration);
            var slotsFromDb = _vaccinationService.GetDoctorActiveSlots(doctorId, dateStart.ToShortDateString());
            while(dateStart + timeSpan <= request.To)
            {
                TimeSlotValidator.Validate(dateStart, dateStart + timeSpan);
                bool isFree = true;
                foreach(var existingSlots in slotsFromDb)
                {
                    if(!TimeSlotValidator.IsAvailable(dateStart, dateStart + timeSpan, existingSlots.From, existingSlots.To))
                    {
                        isFree = false;
                        break;
                    }
                }

                if(isFree)
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
        }

        [HttpPost("doctor/timeSlot/delete/{doctorId}")]
        public void DeleteTimeSlots(Guid doctorId, DeleteTimeSlotsRequest request)
        {
            foreach(var slot in request.Slots)
            {
                if(_vaccinationService.GetDoctorByTimeSlot(slot).Id == doctorId)
                {
                    _vaccinationService.DeleteTimeSlot(slot);
                }
            }
        }

        [HttpPost("doctor/timeSlot/modify/{doctorId}/{timeSlotId}")]
        public void ModifyTimeSlot(Guid doctorId, Guid timeSlotId, ModifyTimeSlotRequest request)
        {
            if(_vaccinationService.GetDoctorByTimeSlot(timeSlotId).Id == doctorId)
            {
                _vaccinationService.ModifyTimeSlot(timeSlotId, request.From, request.To);
            }
        }

        [HttpPost("doctor/vaccinate/confirmVaccination/{doctorId}/{appointmentId}/{batchId}")]

        public ActionResult<ConfirmVaccinationResponse> ConfirmVaccination(Guid doctorId, Guid appointmentId, Guid batchId)
        {
            var appointmentFromDb = _vaccinationService.GetAppointment(appointmentId);
            if (appointmentFromDb is null)
                return BadRequest("Appointment with given id doesn't exist");
            if (appointmentFromDb.TimeSlot_.AssignedDoctor.Id != doctorId)
                return Forbid("User forbidden to confirm vaccination");
            _vaccinationService.ConfirmVaccination(appointmentId);
            var patientFormerAppointmentsFromDb = _vaccinationService.GetFormerAppointments(appointmentFromDb.Patient_.Id);
            int finishedDoses = 0;
            foreach(var appointment in patientFormerAppointmentsFromDb)
            {
                if (appointment.Vaccine_.Id == appointmentFromDb.Vaccine_.Id)
                    finishedDoses++;
            }
            bool isLastDose = finishedDoses == appointmentFromDb.Vaccine_.NumberOfDoses;
            ConfirmVaccinationResponse response = new()
            {
                canCertify = isLastDose,
            };
            return Ok(response);  
        }

    }
}
