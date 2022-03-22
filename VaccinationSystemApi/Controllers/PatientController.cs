using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using VaccinationSystemApi.Models;
using VaccinationSystemApi.Repositories.Interfaces;
using VaccinationSystemApi.Dtos.Patients;

namespace VaccinationSystemApi.Controllers
{
    [ApiController]
    public class PatientController: ControllerBase
    {
        private readonly IVaccinationSystemRepository _vaccinationService;

        public PatientController(IVaccinationSystemRepository repo) => _vaccinationService = repo;     

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
                    ClosingHours = s.ClosingHours,
                    Doctors = s.Doctors,
                    Id = s.Id,
                    Name = s.Name,
                    OpeningHours = s.OpeningHours
                });
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
                var center = _vaccinationService.GetCenterOfDoctor(slot.DoctorId);
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

    }
}
