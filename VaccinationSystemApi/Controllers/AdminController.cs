using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using VaccinationSystemApi.Dtos.Admin;
using VaccinationSystemApi.Repositories.Interfaces;
using VaccinationSystemApi.Helpers;
using VaccinationSystemApi.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace VaccinationSystemApi.Controllers
{
    [ApiController]
    [Route("admin")]
    public class AdminController : ControllerBase
    {
        private readonly IVaccinationSystemRepository _vaccinationService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        public AdminController(IVaccinationSystemRepository repo, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _vaccinationService = repo;
            _roleManager = roleManager;
            _userManager = userManager;
        }
    
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
                    DateOfBirth = patient.DateOfBirth,
                    Active = patient.Active,
                });
            }

            //Unauthorized, Forbidden

            if (patientsDtos.Count == 0)
                return (NotFound("Error, no matching patient found"));

            return Ok(patientsDtos);
        }

        [HttpPost("editPatient")]
        public ActionResult EditPatient(PatientDTO patientToEdit)
        {
            if (!ModelState.IsValid)
                return BadRequest("BadData");

            bool result = _vaccinationService.EditPatient(patientToEdit, out bool wasPatientFound);
            if (!wasPatientFound) return NotFound("Error, no patient found to edit");

            return result ? Ok() : BadRequest("Bad data");
        }

        [HttpDelete("deletePatient/{patientId}")]
        public ActionResult DeletePatient(Guid patientId)
        {
            if (!ModelState.IsValid)
                return BadRequest("BadData");

            bool result = _vaccinationService.RemovePatient(patientId);
            return result ? Ok() : NotFound();
        }

        [HttpGet("doctors")]
        public ActionResult<IEnumerable<DoctorWithCenterDTO>> GetDoctors()
        {
            var doctorsFromDb = _vaccinationService.GetDoctorsWithMatchingVaccinationCentres();
            var doctorDtos = new List<DoctorWithCenterDTO>();

            foreach(var doctor in doctorsFromDb)
            {
                doctorDtos.Add(new DoctorWithCenterDTO
                {
                    Id = doctor.Id,
                    FirstName = doctor.FirstName,
                    LastName = doctor.LastName,
                    Active = doctor.Active,
                    DateOfBirth = doctor.DateOfBirth,
                    Mail = doctor.EMail,
                    PESEL = doctor.Pesel,
                    PhoneNumber = doctor.PhoneNumber,
                    VaccinationCenterID = doctor.VaccinationCenterId,
                    City = doctor.VaccinationCenter_.City,
                    Name = doctor.VaccinationCenter_.Name,
                    Street = doctor.VaccinationCenter_.Address,
                });
            }
            if (doctorDtos.Count == 0)
                return NotFound("Error, no matching doctor found");

            return Ok(doctorDtos);
        }

        [HttpPost("doctors/editDoctor")]
        public ActionResult EditDoctor(DoctorDTO doctorDto)
        {
            bool result = _vaccinationService.EditDoctor(doctorDto, out bool doctorFound);
            if (!doctorFound)
                return NotFound();

            return result ? Ok() : BadRequest();
        }
        [HttpPost("doctors/addDoctor")]
        public async Task<ActionResult> AddDoctor(AddDoctorDTO addDoctorDTO)
        {
            var identityUser = await _userManager.FindByIdAsync(addDoctorDTO.PatientId.ToString());

            if (identityUser is null)
                return BadRequest("");

            await _userManager.AddToRoleAsync(identityUser, "Doctor");

            var doctorToAdd = new Doctor()
            {
                Id = addDoctorDTO.PatientId,
                PatientAccountId = addDoctorDTO.PatientId,
                VaccinationCenterId = addDoctorDTO.VaccinationCenterId
            };

            bool result = _vaccinationService.AddDoctor(doctorToAdd);
            
            return result ? Ok() : BadRequest();
        }

        [HttpDelete("doctors/deleteDoctor/{doctorId}")]
        public ActionResult DeleteDoctor(Guid doctorId)
        {
            bool result = _vaccinationService.DeleteDoctor(doctorId, out bool wasDoctorFound);

            if (!wasDoctorFound)
                return NotFound("Error, no doctor found to delete");

            return result ? Ok() : BadRequest();
        }
    }
}
