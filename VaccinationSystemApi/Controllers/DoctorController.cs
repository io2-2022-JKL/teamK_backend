using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VaccinationSystemApi.Dtos.Doctors;
using VaccinationSystemApi.Models;
using VaccinationSystemApi.Repositories.Interfaces;

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
            _vaccinationService.CreateTimeSlot(new TimeSlot
            {
                Active = true,
                AssignedDoctorId = doctorId,
                From = request.From,
                To = request.To,
                IsFree = true,
                Id = Guid.NewGuid()
            });
        }

        [HttpPost("doctor/timeSlot/delete/{doctorId}")]
        public void DeleteTimeSlots(Guid doctorId, DeleteTimeSlotsRequest request)
        {
            foreach(var slot in request.Slots)
            {
                if(_vaccinationService.GetDoctorByTimeSlot(slot).Id == doctorId)
                {

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
