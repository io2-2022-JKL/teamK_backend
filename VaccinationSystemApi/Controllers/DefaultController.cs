using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using VaccinationSystemApi.Models;
using VaccinationSystemApi.Repositories.Interfaces;
using VaccinationSystemApi.Dtos.Default;
using VaccinationSystemApi.Services;
using System.Threading.Tasks;

namespace VaccinationSystemApi.Controllers
{
    [ApiController]
    public class DefaultController : ControllerBase
    {
        private readonly IVaccinationSystemRepository _vaccinationService;
        private readonly MailService _mailService;
        public DefaultController(IVaccinationSystemRepository repo, MailService mailService)
        {
            _vaccinationService = repo;
            _mailService = mailService;
        }

        [HttpGet("viruses")]
        public ActionResult<IEnumerable<VirusDTO>> GetViruses()
        {
            var virusNames = _vaccinationService.GetViruses();
            List<VirusDTO> virusDtos = new List<VirusDTO>();

            foreach(var name in virusNames)
            {
                virusDtos.Add(new VirusDTO() { Virus = name });
            }
            return Ok(virusDtos);
        }

        [HttpGet("cities")]
        public ActionResult<IEnumerable<CityDTO>> GetCities()
        {
            var cityNames = _vaccinationService.GetCities();
            List<CityDTO> cityDtos = new List<CityDTO>();

            foreach (var name in cityNames)
            {
                cityDtos.Add(new CityDTO() { City = name });
            }
            return Ok(cityDtos);
        }

        [HttpGet("send")]
        public async Task<ActionResult> SendMail()
        {
            _mailService.Send("drabarekpaw@gmail.com", "ŁAŁŁŁć", "ŁAŁŁŁć");
            return Ok();
        }
    }
}
