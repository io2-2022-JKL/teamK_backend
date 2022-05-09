using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaccinationSystemApi.Dtos.Patients
{
    public record BrowseCertificateResponse
    {
        public string Url { get; init; }
        public string VaccineName { get; init; }
        public string VaccineCompany { get; init; }
        public string VirusType { get; init; }
    }
}
