using Microsoft.AspNetCore.Mvc;
using System;
using VaccinationSystemApi.Dtos.Doctors;
using VaccinationSystemApi.Repositories.Interfaces;
using VaccinationSystemApi.Helpers;
using VaccinationSystemApi.Models;

namespace VaccinationSystemApi.Controllers
{
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IVaccinationSystemRepository _vaccinationService;
        public AdminController(IVaccinationSystemRepository repo) => _vaccinationService = repo;
    
        
    }
}
