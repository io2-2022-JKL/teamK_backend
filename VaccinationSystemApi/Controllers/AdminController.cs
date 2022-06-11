using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using VaccinationSystemApi.Dtos.Admin;
using VaccinationSystemApi.Repositories.Interfaces;
using VaccinationSystemApi.Helpers;
using VaccinationSystemApi.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using VaccinationSystemApi.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using VaccinationSystemApi.Services;

namespace VaccinationSystemApi.Controllers
{
    [ApiController]
    [Route("admin")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IVaccinationSystemRepository _vaccinationService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly TimeHoursService _timeHoursService;
        public AdminController(IVaccinationSystemRepository repo, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _vaccinationService = repo;
            _roleManager = roleManager;
            _userManager = userManager;
            _timeHoursService = new TimeHoursService();
        }
    
        [HttpGet("patients")]
        public ActionResult<IEnumerable<EditPatientRequest>> GetPatients()
        {
            try
            {
                var patientsFromDb = _vaccinationService.GetPatients();
                List<EditPatientRequest> patientsDtos = new List<EditPatientRequest>();
                foreach (var patient in patientsFromDb)
                {
                    patientsDtos.Add(new EditPatientRequest()
                    {
                        Id = patient.Id.ToString(),
                        PESEL = patient.Pesel,
                        PhoneNumber = patient.PhoneNumber,
                        Mail = patient.EMail,
                        FirstName = patient.FirstName,
                        LastName = patient.LastName,
                        DateOfBirth = patient.DateOfBirth.ToString("dd-MM-yyyy"),
                        Active = patient.Active,
                    });
                }

                if (patientsDtos.Count == 0)
                    return (NotFound("Error, no matching patient found"));

                return Ok(patientsDtos);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost("patients/editPatient")]
        public async Task<ActionResult> EditPatient(EditPatientRequest patientToEdit)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(patientToEdit.Id);
                if (user is null)
                {
                    return NotFound("Patient to edit not found");
                }

                bool result = _vaccinationService.EditPatient(patientToEdit, out bool wasPatientFound);


                return result ? Ok() : BadRequest("Bad data");
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpDelete("patients/deletePatient/{patientId}")]
        public async Task<ActionResult> DeletePatient(string patientId)
        {
            try
            {
                bool result = _vaccinationService.RemovePatient(Guid.Parse(patientId));
                var user = await _userManager.FindByIdAsync(patientId.ToString());
                await _userManager.DeleteAsync(user);

                return result ? Ok() : NotFound();
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet("doctors")]
        public ActionResult<IEnumerable<DoctorWithCenterDTO>> GetDoctors()
        {
            try
            {
                var doctorsFromDb = _vaccinationService.GetDoctorsWithMatchingVaccinationCentres();
                var doctorDtos = new List<DoctorWithCenterDTO>();

                foreach (var doctor in doctorsFromDb)
                {
                    doctorDtos.Add(new DoctorWithCenterDTO
                    {
                        Id = doctor.Id,
                        FirstName = doctor.FirstName,
                        LastName = doctor.LastName,
                        Active = doctor.Active,
                        DateOfBirth = doctor.DateOfBirth.ToString("dd-MM-yyyy"),
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
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost("doctors/editDoctor")]
        public ActionResult EditDoctor(EditDoctorRequest doctorDto)
        {
            try
            {
                bool result = _vaccinationService.EditDoctor(doctorDto, out bool doctorFound);
                if (!doctorFound)
                    return NotFound();

                return result ? Ok() : BadRequest();
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpPost("doctors/addDoctor")]
        public async Task<ActionResult> AddDoctor(AddDoctorDTO addDoctorDTO)
        {
            try
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
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpDelete("doctors/deleteDoctor/{doctorId}")]
        public ActionResult DeleteDoctor(Guid doctorId)
        {
            try
            {
                bool result = _vaccinationService.DeleteDoctor(doctorId, out bool wasDoctorFound);

                if (!wasDoctorFound)
                    return NotFound("Error, no doctor found to delete");

                return result ? Ok() : BadRequest();
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet("doctors/timeSlots/{doctorId}")]
        public ActionResult<IEnumerable<TimeslotDTO>> GetTimeslots(Guid doctorId)
        {
            try
            {
                List<TimeslotDTO> timeslotsDtos = new List<TimeslotDTO>();
                var timeslots = _vaccinationService.GetDoctorsTimeSlots(doctorId);

                foreach(var timeslot in timeslots)
                {
                    var timeslotDto = new TimeslotDTO()
                    {
                        Active = timeslot.Active,
                        From = timeslot.From.ToString("dd-MM-yyyy HH:mm"),
                        To = timeslot.To.ToString("dd-MM-yyyy HH:mm"),
                        Id = timeslot.Id,
                        IsFree = timeslot.IsFree,
                    };

                    timeslotsDtos.Add(timeslotDto);
                }


                return Ok(timeslotsDtos);
            }
            catch (ModelNotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("doctors/timeSlots/deleteTimeSlots")]
        public ActionResult DeleteTimeslots(IEnumerable<TimeslotIdWrapperDTO> timeslotsToDelete)
        {
            List<Guid> timeslotIds = new List<Guid>();

            foreach(var timeslot in timeslotsToDelete)
            {
                timeslotIds.Add(timeslot.Id);
            }

            try
            {
                _vaccinationService.DeleteTimeslots(timeslotIds);
                return Ok();
            }
            catch (ModelNotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("vaccinationCenters")]
        public ActionResult<IEnumerable<VaccinationCenterAdminDTO>> GetVaccinationCenters()
        {
            try
            {
                var vaccinationCenters = _vaccinationService.GetVaccinationCenters();
                List<VaccinationCenterAdminDTO> vaccCentersDtos = new List<VaccinationCenterAdminDTO>();
                foreach (var vc in vaccinationCenters)
                {
                    List<VaccineAdminDTO> vaccineDtos = new List<VaccineAdminDTO>();
                    var vaccinesFromDb = _vaccinationService.GetVaccinesInCenter(vc.Id);
                    foreach (var vaccine in vaccinesFromDb)
                    {
                        var vaccineDto = new VaccineAdminDTO()
                        {
                            CompanyName = vaccine.Company,
                            Id = vaccine.Id,
                            Name = vaccine.Name,
                            Virus = _vaccinationService.GetVaccineVirus(vaccine.Id).Name,
                        };
                        vaccineDtos.Add(vaccineDto);
                    }

                    var openingHoursDto = _timeHoursService.OpeningHoursToDTO(vc.OpeningHours_);
                    vaccCentersDtos.Add(
                        new VaccinationCenterAdminDTO()
                        {
                            Active = vc.Active,
                            City = vc.City,
                            Id = vc.Id,
                            Name = vc.Name,
                            Street = vc.Address,
                            OpeningHoursDays = _timeHoursService.DTOToAdminDTO(openingHoursDto),
                            Vaccines = vaccineDtos
                        });
                }

                return Ok(vaccCentersDtos);
            }
            catch (ModelNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost("vaccinationCenters/addVaccinationCenter")]
        public ActionResult<AddVaccinationCenterResponse> AddVaccinationCenter(AddVaccinationCenterRequest centerToAdd)
        {
            try
            {
                _vaccinationService.AddVaccinationCenter(centerToAdd);
                return Ok();
                AddVaccinationCenterResponse respone = new()
                {
                    city = centerToAdd.City,
                    active = centerToAdd.Active,
                    name = centerToAdd.Name,
                    openingHoursDays = centerToAdd.OpeningHoursDays,
                    street = centerToAdd.Street,
                    vaccineIds = centerToAdd.VaccineIds
                };
                return Ok(respone);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost("vaccinationCenters/editVaccinationCenter")]
        public ActionResult EditVaccinationCenter(EditVaccinationCenterAdminRequest centerToEdit)
        {
            try
            {
                _vaccinationService.EditVaccinationCenter(centerToEdit);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("vaccinationCenters/deleteVaccinationCenter/{vaccinationCenterId}")]
        public ActionResult DeleteVaccinationCenter(Guid vaccinationCenterId)
        {
            try
            {
                _vaccinationService.DeleteVaccinationCenter(vaccinationCenterId);
                return Ok();
            }
            catch (NoChangesInDatabaseException)
            {
                return NotFound();
            }
        }

        [HttpGet("vaccines")]
        public ActionResult<IEnumerable<VaccineExtendedDTO>> GetVaccines()
        {
            try
            {
                IEnumerable<Vaccine> vaccinesFromDb = _vaccinationService.GetExtendedVaccines();
                List<VaccineExtendedDTO> vaccinesDto = new List<VaccineExtendedDTO>();

                foreach (var v in vaccinesFromDb)
                {
                    vaccinesDto.Add(new VaccineExtendedDTO()
                    {
                        VaccineId = v.Id,
                        Active = v.IsStillBeingUsed,
                        Company = v.Company,
                        MaxDaysBetweenDoses = v.MaxDaysBetweenDoses,
                        MaxPatientAge = v.MaxPatientAge,
                        MinDaysBetweenDoses = v.MinDaysBetweenDoses,
                        MinPatientAge = v.MinPatientAge,
                        Name = v.Name,
                        NumberOfDoses = v.NumberOfDoses,
                        Virus = v.Virus_.Name
                    });
                }

                return Ok(vaccinesDto);
            }
            catch (ModelNotFoundException)
            {
                return NotFound("Data not found");
            }
        }
        [HttpPost("vaccines/addVaccine")]
        public ActionResult AddVaccine(AddVaccineRequest vaccineToAdd)
        {
            try
            {
                _vaccinationService.AddVaccine(vaccineToAdd);
                return Ok();
            }
            catch (InvalidOperationException)
            {
                return BadRequest("Error, bad request syntax. Perhaps you tried to add a non-existant virus?");
            }
            catch (NoChangesInDatabaseException)
            {
                return NotFound();
            }
            
        }
        [HttpPost("vaccines/editVaccine")]
        public ActionResult EditVaccine(VaccineExtendedDTO vaccine)
        {
            try
            {
                _vaccinationService.EditVaccine(vaccine);
            }
            catch (NoChangesInDatabaseException)
            {
                return NotFound("Error, no vaccine found to edit");
            }
            catch(Exception ex)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete("vaccines/deleteVaccine/{vaccineId}")]
        public ActionResult DeleteVaccine(Guid vaccineId)
        {
            try
            {
                _vaccinationService.DeleteVaccine(vaccineId);
                return Ok();
            }
            catch (NoChangesInDatabaseException)
            {
                return NotFound("Data not found");
            }
            
        }
    }
}
