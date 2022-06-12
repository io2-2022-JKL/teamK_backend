using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaccinationSystemApi.Dtos.Patients
{
    public record BrowseCertificateResponse
    {
        public string url { get; init; }
        public string vaccineName { get; init; }
        public string vaccineCompany { get; init; }
        public string virusType { get; init; }
    }
}
