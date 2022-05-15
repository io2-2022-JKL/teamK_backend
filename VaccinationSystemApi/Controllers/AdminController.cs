using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using VaccinationSystemApi.Dtos.Admin;
using VaccinationSystemApi.Repositories.Interfaces;
using VaccinationSystemApi.Helpers;
using VaccinationSystemApi.Models;

namespace VaccinationSystemApi.Controllers
{
    [ApiController]
    [Route("admin")]
    public class AdminController : ControllerBase
    {
        private readonly IVaccinationSystemRepository _vaccinationService;
        public AdminController(IVaccinationSystemRepository repo) => _vaccinationService = repo;
    
        [HttpGet("patients")]
        public ActionResult<IEnumerable<PatientDTO>> GetPatients()
        {
            var patientsFromDb = _vaccinationService.GetPatients();
            List<PatientDTO> patientsDtos = new List<PatientDTO>();
            foreach(var patient in patientsFromDb)
            {
                patientsDtos.Add(new PatientDTO()
                {
                    PatientId = patient.Id.ToString(),
                    PESEL = patient.Pesel,
                    PhoneNumber = patient.PhoneNumber,
                    Mail = patient.EMail,
                    FirstName = patient.FirstName,
                    LastName = patient.LastName,
                    DateOfBirth = patient.DateOfBirth.ToShortDateString(),
                    Active = patient.Active,
                });
            }

            //Unauthorized, Forbidden

            if (patientsDtos.Count == 0)
                return (NotFound("Error, no matching patient found"));

            return Ok(patientsDtos);
        }
    }
}
