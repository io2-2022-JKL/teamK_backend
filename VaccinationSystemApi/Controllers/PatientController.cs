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
        private readonly IVaccinationSystemRepository _patientRepository;

        public PatientController(IVaccinationSystemRepository repo) => _patientRepository = repo;

        [HttpGet("patients")]
        
        public IEnumerable<Patient> GetPatients()
        {
            return _patientRepository.GetPatients();
        }

        [HttpGet("patients/{id}")]
        public Patient GetPatient(Guid id)
        {
            return _patientRepository.GetPatient(id);
        }

        [HttpGet("centers/{city}")]

        public IEnumerable<VaccinationCenter> BrowseVaccinationCenters(string city)
        {
            return _patientRepository.GetCenters().Where(x => x.City == city);
        }

        [HttpGet("timeSlots/{id}")]
        public IEnumerable<TimeSlot> GetTimeSlots(Guid id, DateTime date)
        {
            var slotsFromDb = _patientRepository.GetTimeSlots();
            List<TimeSlot> searched = new List<TimeSlot>();

            foreach(var slot in slotsFromDb)
            {
                var center = _patientRepository.GetCenterOfDoctor(slot.DoctorId);
                var slotDate = slot.From.Date;
                if (DateTime.Compare(date, slotDate) == 0 && center.Id == id)
                    searched.Add(slot);
            }

            return searched;
        }

    }
}
