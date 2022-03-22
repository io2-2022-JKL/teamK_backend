using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VaccinationSystemApi.Models;
using VaccinationSystemApi.Repositories.Interfaces;

namespace VaccinationSystemApi.Controllers
{
    [ApiController]
    public class PatientController: ControllerBase
    {
        private readonly IVaccinationSystemRepository _vaccinationService;

        public PatientController(IVaccinationSystemRepository repo) => _vaccinationService = repo;

        [HttpGet("patients")]
        
        public IEnumerable<Patient> GetPatients()
        {
            return _vaccinationService.GetPatients();
        }

        [HttpGet("patients/{id}")]
        public Patient GetPatient(Guid id)
        {
            return _vaccinationService.GetPatient(id);
        }

        [HttpGet("centers/{city}")]

        public IEnumerable<VaccinationCenter> BrowseVaccinationCenters(string city)
        {
            return _vaccinationService.GetCenters().Where(x => x.City == city);
        }

        [HttpGet("timeSlots/{id}")]
        public IEnumerable<TimeSlot> GetTimeSlots(Guid id, DateTime date)
        {
            var slotsFromDb = _vaccinationService.GetTimeSlots();
            List<TimeSlot> searched = new List<TimeSlot>();

            foreach(var slot in slotsFromDb)
            {
                var center = _vaccinationService.GetCenterOfDoctor(slot.DoctorId);
                var slotDate = slot.From.Date;
                if (DateTime.Compare(date, slotDate) == 0 && center.Id == id)
                    searched.Add(slot);
            }

            return searched;
        }

        [HttpPost("appointments/create/{patientId}/{timeSlotId}/{vaccineId}")]
        public Guid MakeAppointment(Guid patientId, Guid timeSlotId, Guid vaccineId)
        {
            return _vaccinationService.CreateAppointment(patientId, timeSlotId, vaccineId);
        }

    }
}
