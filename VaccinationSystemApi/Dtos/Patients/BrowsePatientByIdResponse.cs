using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaccinationSystemApi.Dtos.Patients
{
    public record BrowsePatientByIdResponse
    {
        public string firstName { get; init; }
        public string lastName { get; init; }
        public string PESEL { get; init; }
        public DateTime dateOfBirth { get; init; }
        public string mail { get; init; }
        public string phoneNumber { get; init; }
    }
}
