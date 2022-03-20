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
        private readonly IPatientRepository _patientRepository;

        public PatientController(IPatientRepository repo) => _patientRepository = repo;

        [HttpGet("patients")]
        
        public ICollection<Patient> GetPatients()
        {
            return _patientRepository.GetPatients();
        }

        [HttpGet("patients/{id}")]
        public Patient GetPatient(Guid id)
        {
            return _patientRepository.GetPatient(id);
        }

    }
}
