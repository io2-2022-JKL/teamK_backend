using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaccinationSystemApi.Dtos.Doctors
{
    public record GetDoctorInfoResponse
    {
        public string vaccinationCenterId { get; init; }
        public string vaccinationCenterName { get; init; }
        public string vaccinationCenterCity { get; init; }
        public string vaccinationCenterStreet { get; init; }
        public string patientAccountId { get; init; }
    }
}
