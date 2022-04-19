using Microsoft.AspNetCore.Mvc;
using System;
using VaccinationSystemApi.Dtos.Doctors;
using VaccinationSystemApi.Repositories.Interfaces;
using VaccinationSystemApi.Helpers;
using VaccinationSystemApi.Models;

namespace VaccinationSystemApi.Controllers
{

    [ApiController]

    public class DoctorController: ControllerBase
    {
        private readonly IVaccinationSystemRepository _vaccinationService;
        public DoctorController(IVaccinationSystemRepository repo) => _vaccinationService = repo;

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

    }
}
