using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaccinationSystemApi.Dtos.Doctors
{
    public record GetDoctorInfoResponse
    {
        public string VaccinationCenterId { get; init; }
        public string VaccinationCenterName { get; init; }    
        public string VaccinationCenterStreet { get; init; }
        public string PatientAccountId { get; init; }
    }
}
