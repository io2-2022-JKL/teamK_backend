using Microsoft.AspNetCore.Mvc;
using K_VaccinationSystem_backend.DTOs.Patient;

namespace K_VaccinationSystem_backend.Controllers
{
    [Route("patient")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        [HttpGet("basic")]
        public BasicDTO Basic()
        {
            return new BasicDTO() { Name = "Jerry" };
        }
    }
}